using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Zero.Stores;
using Zero.Shared;

namespace Zero.Web.Pages.Stores
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public string UrlFilter { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IStoreAppService _storeAppService;

        public IndexModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}