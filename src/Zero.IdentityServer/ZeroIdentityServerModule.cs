using System;
using System.IO;
using System.Linq;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zero.EntityFrameworkCore;
using Zero.Localization;
using Zero.MultiTenancy;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.VirtualFileSystem;

namespace Zero
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAccountPublicWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiLeptonThemeModule),
        typeof(AbpAccountPublicApplicationModule),
        typeof(ZeroEntityFrameworkCoreDbMigrationsModule)
        )]
    public class ZeroIdentityServerModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ZeroResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });

            Configure<AbpAuditingOptions>(options =>
            {
                //options.IsEnabledForGetRequests = true;
                options.ApplicationName = "AuthServer";
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<ZeroDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Zero.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<ZeroDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}Zero.Domain", Path.DirectorySeparatorChar)));
                });
            }

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });

            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "Zero:";
            });

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "Zero-Protection-Keys");
            }

            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
            }

            app.UseCors(DefaultCorsPolicyName);
            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAbpRequestLocalization();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAuditing();
            app.UseConfiguredEndpoints();
        }
    }
}
