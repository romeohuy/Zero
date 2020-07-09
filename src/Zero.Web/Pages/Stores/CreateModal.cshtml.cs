using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zero.Stores;

namespace Zero.Web.Pages.Stores
{
    public class CreateModalModel : ZeroPageModel
    {
        [BindProperty]
        public StoreCreateDto Store { get; set; }

        private readonly IStoreAppService _storeAppService;

        public CreateModalModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        public async Task OnGetAsync()
        {
            Store = new StoreCreateDto();
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storeAppService.CreateAsync(Store);
            return NoContent();
        }
    }
}