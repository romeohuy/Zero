using Zero.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Zero.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ZeroController : AbpController
    {
        protected ZeroController()
        {
            LocalizationResource = typeof(ZeroResource);
        }
    }
}