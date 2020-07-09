using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Zero.Stores
{
    public interface IStoreRepository : IRepository<Store, Guid>
    {
        Task<List<Store>> GetListAsync(
            string filterText = null,
            string name = null,
            string url = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string url = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default);
    }
}