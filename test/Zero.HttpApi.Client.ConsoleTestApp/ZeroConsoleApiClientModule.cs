using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Zero.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(ZeroHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ZeroConsoleApiClientModule : AbpModule
    {
        
    }
}
