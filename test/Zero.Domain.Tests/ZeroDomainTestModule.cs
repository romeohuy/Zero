using Zero.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Zero
{
    [DependsOn(
        typeof(ZeroEntityFrameworkCoreTestModule)
        )]
    public class ZeroDomainTestModule : AbpModule
    {

    }
}