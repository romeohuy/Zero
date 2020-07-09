using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Zero.Localization;
using Zero.Permissions;
using Volo.Abp.Account.Localization;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.IdentityServer.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace Zero.Web.Menus
{
    public class ZeroMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public ZeroMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<ZeroResource>();
            //Home
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    ZeroMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fa fa-home",
                    order: 1
                )
            );
            if (await context.IsGrantedAsync(ZeroPermissions.Dashboard.Host))
            {
                //HostDashboard
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        ZeroMenus.HostDashboard,
                        l["Menu:Dashboard"],
                        "~/HostDashboard",
                        icon: "fa fa-line-chart",
                        order: 2
                    )
                );
            }

            if (await context.IsGrantedAsync(ZeroPermissions.Dashboard.Tenant))
            {
                //TenantDashboard
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        ZeroMenus.TenantDashboard,
                        l["Menu:Dashboard"],
                        "~/Dashboard",
                        icon: "fa fa-line-chart",
                        order: 2
                    )
                );
            }

            //Administration
            var administration = context.Menu.GetAdministration();
            administration.Order = 3;

            //Administration->Saas
            administration.SetSubItemOrder(SaasHostMenuNames.GroupName, 1);

            //Administration->Identity
            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);

            //Administration->Identity Server
            administration.SetSubItemOrder(AbpIdentityServerMenuNames.GroupName, 3);

            //Administration->Language Management
            administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 4);

            //Administration->Text Template Management
            administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 5);

            //Administration->Audit Logs
            administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 6);

            //Administration->Settings
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);

            if (await context.IsGrantedAsync(ZeroPermissions.Stores.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        "Zero.Stores",
                        l["Menu:Stores"],
                        url: "/Stores",
                        icon: "fa fa-file-alt")
                );
            }

            if (await context.IsGrantedAsync(ZeroPermissions.StoreUsers.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem(
                        "Zero.StoreUsers",
                        l["Menu:StoreUsers"],
                        url: "/StoreUsers",
                        icon: "fa fa-file-alt")
                );
            }
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<ZeroResource>();
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountStringLocalizer["ManageYourProfile"], $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, null, "_blank"));
            context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));

            return Task.CompletedTask;
        }
    }
}