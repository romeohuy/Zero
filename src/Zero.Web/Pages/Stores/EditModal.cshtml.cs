using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Zero.Stores;

namespace Zero.Web.Pages.Stores
{
    public class EditModalModel : ZeroPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public StoreUpdateDto Store { get; set; }

        private readonly IStoreAppService _storeAppService;

        public EditModalModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        public async Task OnGetAsync()
        {
            var store = await _storeAppService.GetAsync(Id);
            Store = ObjectMapper.Map<StoreDto, StoreUpdateDto>(store);

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storeAppService.UpdateAsync(Id, Store);
            return NoContent();
        }
    }
}