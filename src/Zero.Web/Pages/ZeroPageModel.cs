using Zero.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Zero.Web.Pages
{
    /* Inherit your Page Model classes from this class.
     */
    public abstract class ZeroPageModel : AbpPageModel
    {
        protected ZeroPageModel()
        {
            LocalizationResourceType = typeof(ZeroResource);
        }
    }
}