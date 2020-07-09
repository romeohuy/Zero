using Zero.Localization;
using Volo.Abp.Application.Services;

namespace Zero
{
    /* Inherit your application services from this class.
     */
    public abstract class ZeroAppService : ApplicationService
    {
        protected ZeroAppService()
        {
            LocalizationResource = typeof(ZeroResource);
        }
    }
}
