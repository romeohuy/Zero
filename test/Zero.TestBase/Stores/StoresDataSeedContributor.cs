using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zero.Stores;

namespace Zero.Stores
{
    public class StoresDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IStoreRepository _storeRepository;

        public StoresDataSeedContributor(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _storeRepository.InsertAsync(new Store
            (
                id: Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"),
                name: "8c62f167554a472facb722924f48f3c06941f5547c09460d9a06bc614d79b8f0d24fa5ca0",
                url: "169189c2a39a413fa74bddd6ee0f5cc5db9527089823401284f6b2e30ad9ad55348c095996fe476187d42b792b8189",
                isActive: true
            ));

            await _storeRepository.InsertAsync(new Store
            (
                id: Guid.Parse("723dabc2-a59f-45ae-b608-a55aa04736d3"),
                name: "49f68f4383614b6a9d4015a5eec22782d11e3236",
                url: "532003024f0c4ff9af860096a9fdf2e",
                isActive: true
            ));
        }
    }
}