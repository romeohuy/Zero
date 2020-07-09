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

namespace Zero.StoreUsers
{
    public class EfCoreStoreUserRepository : EfCoreRepository<ZeroDbContext, StoreUser, Guid>, IStoreUserRepository
    {
        public EfCoreStoreUserRepository(IDbContextProvider<ZeroDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<StoreUserWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetQueryForNavigationProperties()
                .FirstOrDefaultAsync(e => e.StoreUser.Id == id, GetCancellationToken(cancellationToken));
        }

        public async Task<List<StoreUserWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string desc = null,
            bool? isActive = null,
            Guid? storeId = null,
            Guid? appUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryForNavigationProperties();
            query = ApplyFilter(query, filterText, desc, isActive, storeId, appUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? StoreUserConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<StoreUserWithNavigationProperties> GetQueryForNavigationProperties()
        {
            return from storeUser in DbSet
                   join store in DbContext.Stores on storeUser.StoreId equals store.Id into stores
                   from store in stores.DefaultIfEmpty()
                   join appUser in DbContext.Users on storeUser.AppUserId equals appUser.Id into users
                   from appUser in users.DefaultIfEmpty()

                   select new StoreUserWithNavigationProperties
                   {
                       StoreUser = storeUser,
                       Store = store,
                       AppUser = appUser
                   };
        }

        protected virtual IQueryable<StoreUserWithNavigationProperties> ApplyFilter(
            IQueryable<StoreUserWithNavigationProperties> query,
            string filterText,
            string desc = null,
            bool? isActive = null,
            Guid? storeId = null,
            Guid? appUserId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.StoreUser.Desc.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(desc), e => e.StoreUser.Desc.Contains(desc))
                    .WhereIf(isActive.HasValue, e => e.StoreUser.IsActive == isActive)
                    .WhereIf(storeId != null && storeId != Guid.Empty, e => e.Store != null && e.Store.Id == storeId)
                    .WhereIf(appUserId != null && appUserId != Guid.Empty, e => e.AppUser != null && e.AppUser.Id == appUserId);
        }

        public async Task<List<StoreUser>> GetListAsync(
            string filterText = null,
            string desc = null,
            bool? isActive = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(DbSet, filterText, desc, isActive);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? StoreUserConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string desc = null,
            bool? isActive = null,
            Guid? storeId = null,
            Guid? appUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryForNavigationProperties();
            query = ApplyFilter(query, filterText, desc, isActive, storeId, appUserId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<StoreUser> ApplyFilter(
            IQueryable<StoreUser> query,
            string filterText,
            string desc = null,
            bool? isActive = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Desc.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(desc), e => e.Desc.Contains(desc))
                    .WhereIf(isActive.HasValue, e => e.IsActive == isActive);
        }
    }
}