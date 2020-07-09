using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zero.EntityFrameworkCore;

namespace Zero.Stores
{
    public class EfCoreStoreRepository : EfCoreRepository<ZeroDbContext, Store, Guid>, IStoreRepository
    {
        public EfCoreStoreRepository(IDbContextProvider<ZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Store>> GetListAsync(
            string filterText = null,
            string name = null,
            string url = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(DbSet, filterText, name, url, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? StoreConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string url = null,
            bool? isActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(DbSet, filterText, name, url, isActive);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Store> ApplyFilter(
            IQueryable<Store> query,
            string filterText,
            string name = null,
            string url = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.Url.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(url), e => e.Url.Contains(url))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}