using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Zero.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Zero.Web.Pages
{
    /* Inherit your Pages (.cshtml files in the Pages folder) from this class.
     * In this way, you can use some shared properties & methods. Use
     *
     * @inherits Zero.Web.Pages.ZeroPage
     *
     * in your .cshtml Page files.
     */
    public abstract class ZeroPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<ZeroResource> L { get; set; }
    }
}
