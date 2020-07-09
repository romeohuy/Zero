using Zero.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zero.StoreUsers;

namespace Zero.Web.Pages.StoreUsers
{
    public class CreateModalModel : ZeroPageModel
    {
        [BindProperty]
        public StoreUserCreateDto StoreUser { get; set; }

        public List<SelectListItem> StoreLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("", Guid.Empty.ToString())
        };

        private readonly IStoreUserAppService _storeUserAppService;

        public CreateModalModel(IStoreUserAppService storeUserAppService)
        {
            _storeUserAppService = storeUserAppService;
        }

        public async Task OnGetAsync()
        {
            StoreUser = new StoreUserCreateDto();
            StoreLookupList.AddRange(
                            (await _storeUserAppService.GetStoreLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items
                            .Select(t => new SelectListItem(t.DisplayName, t.Id.ToString()))
                            .ToList());

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storeUserAppService.CreateAsync(StoreUser);
            return NoContent();
        }
    }
}