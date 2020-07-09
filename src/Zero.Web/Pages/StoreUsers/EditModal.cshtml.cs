using Zero.Shared;
using Zero.Users;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Zero.StoreUsers;

namespace Zero.Web.Pages.StoreUsers
{
    public class EditModalModel : ZeroPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public StoreUserUpdateDto StoreUser { get; set; }

        public AppUserDto AppUser { get; set; }
        public List<SelectListItem> StoreLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("", Guid.Empty.ToString())
        };

        private readonly IStoreUserAppService _storeUserAppService;

        public EditModalModel(IStoreUserAppService storeUserAppService)
        {
            _storeUserAppService = storeUserAppService;
        }

        public async Task OnGetAsync()
        {
            var storeUserWithNavigationPropertiesDto = await _storeUserAppService.GetWithNavigationPropertiesAsync(Id);
            StoreUser = ObjectMapper.Map<StoreUserDto, StoreUserUpdateDto>(storeUserWithNavigationPropertiesDto.StoreUser);

            AppUser = storeUserWithNavigationPropertiesDto.AppUser;
            StoreLookupList.AddRange(
                (await _storeUserAppService.GetStoreLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items
                .Select(t => new SelectListItem(t.DisplayName, t.Id.ToString()))
                .ToList());

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storeUserAppService.UpdateAsync(Id, StoreUser);
            return NoContent();
        }
    }
}