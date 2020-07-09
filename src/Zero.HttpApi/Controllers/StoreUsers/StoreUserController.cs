using Zero.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Zero.StoreUsers;

namespace Zero.Controllers.StoreUsers
{
    [RemoteService]
    [Area("app")]
    [ControllerName("StoreUser")]
    [Route("api/app/storeUser")]
    public class StoreUserController : AbpController, IStoreUserAppService
    {
        private readonly IStoreUserAppService _storeUserAppService;

        public StoreUserController(IStoreUserAppService storeUserAppService)
        {
            _storeUserAppService = storeUserAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<StoreUserWithNavigationPropertiesDto>> GetListAsync(GetStoreUsersInput input)
        {
            return _storeUserAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<StoreUserWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _storeUserAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StoreUserDto> GetAsync(Guid id)
        {
            return _storeUserAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("store-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetStoreLookupAsync(LookupRequestDto input)
        {
            return _storeUserAppService.GetStoreLookupAsync(input);
        }
        [HttpGet]
        [Route("appUser-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetAppUserLookupAsync(LookupRequestDto input)
        {
            return _storeUserAppService.GetAppUserLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<StoreUserDto> CreateAsync(StoreUserCreateDto input)
        {
            return _storeUserAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StoreUserDto> UpdateAsync(Guid id, StoreUserUpdateDto input)
        {
            return _storeUserAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _storeUserAppService.DeleteAsync(id);
        }
    }
}