using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Zero.Stores
{
    public interface IStoreAppService : IApplicationService
    {
        Task<PagedResultDto<StoreDto>> GetListAsync(GetStoresInput input);

        Task<StoreDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<StoreDto> CreateAsync(StoreCreateDto input);

        Task<StoreDto> UpdateAsync(Guid id, StoreUpdateDto input);
    }
}