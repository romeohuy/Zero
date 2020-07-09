using Zero.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Zero.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ZeroEntityFrameworkCoreDbMigrationsModule),
        typeof(ZeroApplicationContractsModule)
    )]
    public class ZeroDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }
}