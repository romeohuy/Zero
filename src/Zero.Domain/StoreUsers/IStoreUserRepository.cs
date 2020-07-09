using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Zero.StoreUsers
{
    public interface IStoreUserRepository : IRepository<StoreUser, Guid>
    {
        Task<StoreUserWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<StoreUserWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string desc = null,
            bool? isActive = null,
            Guid? storeId = null,
            Guid? appUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<StoreUser>> GetListAsync(
                    string filterText = null,
                    string desc = null,
                    bool? isActive = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string desc = null,
            bool? isActive = null,
            Guid? storeId = null,
            Guid? appUserId = null,
            CancellationToken cancellationToken = default);
    }
}