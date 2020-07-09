using Zero.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zero.StoreUsers
{
    public interface IStoreUserAppService : IApplicationService
    {
        Task<PagedResultDto<StoreUserWithNavigationPropertiesDto>> GetListAsync(GetStoreUsersInput input);

        Task<StoreUserWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<StoreUserDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetStoreLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetAppUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<StoreUserDto> CreateAsync(StoreUserCreateDto input);

        Task<StoreUserDto> UpdateAsync(Guid id, StoreUserUpdateDto input);
    }
}