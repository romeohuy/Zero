using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Zero.EntityFrameworkCore
{
    [DependsOn(
        typeof(ZeroEntityFrameworkCoreModule)
    )]
    public class ZeroEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ZeroMigrationsDbContext>();
        }
    }
}