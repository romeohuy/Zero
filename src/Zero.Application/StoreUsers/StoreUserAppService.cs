using Zero.Shared;
using Zero.Users;
using Zero.Stores;
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
using Zero.StoreUsers;

namespace Zero.StoreUsers
{
    [RemoteService(IsEnabled = false)]
    [Authorize(ZeroPermissions.StoreUsers.Default)]
    public class StoreUserAppService : ApplicationService, IStoreUserAppService
    {
        private readonly IStoreUserRepository _storeUserRepository;
        private readonly IRepository<Store, Guid> _storeRepository;
        private readonly IRepository<AppUser, Guid> _appUserRepository;

        public StoreUserAppService(IStoreUserRepository storeUserRepository, IRepository<Store, Guid> storeRepository, IRepository<AppUser, Guid> appUserRepository)
        {
            _storeUserRepository = storeUserRepository; _storeRepository = storeRepository;
            _appUserRepository = appUserRepository;
        }

        public virtual async Task<PagedResultDto<StoreUserWithNavigationPropertiesDto>> GetListAsync(GetStoreUsersInput input)
        {
            var totalCount = await _storeUserRepository.GetCountAsync(input.FilterText, input.Desc, input.IsActive, input.StoreId, input.AppUserId);
            var items = await _storeUserRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Desc, input.IsActive, input.StoreId, input.AppUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<StoreUserWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<StoreUserWithNavigationProperties>, List<StoreUserWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<StoreUserWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<StoreUserWithNavigationProperties, StoreUserWithNavigationPropertiesDto>
                (await _storeUserRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<StoreUserDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<StoreUser, StoreUserDto>(await _storeUserRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetStoreLookupAsync(LookupRequestDto input)
        {
            var query = _storeRepository.AsQueryable()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Store>();

            var totalCount = query.Count();

            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Store>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetAppUserLookupAsync(LookupRequestDto input)
        {
            var query = _appUserRepository.AsQueryable()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Email != null && x.Email.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<AppUser>();

            var totalCount = query.Count();

            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppUser>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(ZeroPermissions.StoreUsers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _storeUserRepository.DeleteAsync(id);
        }

        [Authorize(ZeroPermissions.StoreUsers.Create)]
        public virtual async Task<StoreUserDto> CreateAsync(StoreUserCreateDto input)
        {
            var storeUser = ObjectMapper.Map<StoreUserCreateDto, StoreUser>(input);

            storeUser = await _storeUserRepository.InsertAsync(storeUser, autoSave: true);
            return ObjectMapper.Map<StoreUser, StoreUserDto>(storeUser);
        }

        [Authorize(ZeroPermissions.StoreUsers.Edit)]
        public virtual async Task<StoreUserDto> UpdateAsync(Guid id, StoreUserUpdateDto input)
        {
            var storeUser = await _storeUserRepository.GetAsync(id);
            ObjectMapper.Map(input, storeUser);
            storeUser = await _storeUserRepository.UpdateAsync(storeUser);
            return ObjectMapper.Map<StoreUser, StoreUserDto>(storeUser);
        }
    }
}