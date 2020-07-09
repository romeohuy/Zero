using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Zero.StoreUsers;
using Zero.Shared;

namespace Zero.Web.Pages.StoreUsers
{
    public class IndexModel : AbpPageModel
    {
        public string DescFilter { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(StoreLookupList))]
        public Guid? StoreIdFilter { get; set; }
        public List<SelectListItem> StoreLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("", Guid.Empty.ToString())
        };

        private readonly IStoreUserAppService _storeUserAppService;

        public IndexModel(IStoreUserAppService storeUserAppService)
        {
            _storeUserAppService = storeUserAppService;
        }

        public async Task OnGetAsync()
        {
            StoreLookupList.AddRange(
        (await _storeUserAppService.GetStoreLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items
        .Select(t => new SelectListItem(t.DisplayName, t.Id.ToString()))
        .ToList());

            await Task.CompletedTask;
        }
    }
}