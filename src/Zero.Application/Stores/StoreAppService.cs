using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Zero.Permissions;
using Zero.Stores;

namespace Zero.Stores
{
    [RemoteService(IsEnabled = false)]
    [Authorize(ZeroPermissions.Stores.Default)]
    public class StoreAppService : ApplicationService, IStoreAppService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreAppService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public virtual async Task<PagedResultDto<StoreDto>> GetListAsync(GetStoresInput input)
        {
            var totalCount = await _storeRepository.GetCountAsync(input.FilterText, input.Name, input.Url, input.IsActive);
            var items = await _storeRepository.GetListAsync(input.FilterText, input.Name, input.Url, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<StoreDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Store>, List<StoreDto>>(items)
            };
        }

        public virtual async Task<StoreDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Store, StoreDto>(await _storeRepository.GetAsync(id));
        }

        [Authorize(ZeroPermissions.Stores.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _storeRepository.DeleteAsync(id);
        }

        [Authorize(ZeroPermissions.Stores.Create)]
        public virtual async Task<StoreDto> CreateAsync(StoreCreateDto input)
        {
            var store = ObjectMapper.Map<StoreCreateDto, Store>(input);

            store = await _storeRepository.InsertAsync(store, autoSave: true);
            return ObjectMapper.Map<Store, StoreDto>(store);
        }

        [Authorize(ZeroPermissions.Stores.Edit)]
        public virtual async Task<StoreDto> UpdateAsync(Guid id, StoreUpdateDto input)
        {
            var store = await _storeRepository.GetAsync(id);
            ObjectMapper.Map(input, store);
            store = await _storeRepository.UpdateAsync(store);
            return ObjectMapper.Map<Store, StoreDto>(store);
        }
    }
}