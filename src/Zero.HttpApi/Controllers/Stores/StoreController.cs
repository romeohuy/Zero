using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Zero.Stores;

namespace Zero.Controllers.Stores
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Store")]
    [Route("api/app/store")]
    public class StoreController : AbpController, IStoreAppService
    {
        private readonly IStoreAppService _storeAppService;

        public StoreController(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<StoreDto>> GetListAsync(GetStoresInput input)
        {
            return _storeAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StoreDto> GetAsync(Guid id)
        {
            return _storeAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<StoreDto> CreateAsync(StoreCreateDto input)
        {
            return _storeAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StoreDto> UpdateAsync(Guid id, StoreUpdateDto input)
        {
            return _storeAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _storeAppService.DeleteAsync(id);
        }
    }
}