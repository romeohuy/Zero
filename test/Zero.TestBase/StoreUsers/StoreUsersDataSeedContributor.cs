using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zero.StoreUsers;

namespace Zero.StoreUsers
{
    public class StoreUsersDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IStoreUserRepository _storeUserRepository;

        public StoreUsersDataSeedContributor(IStoreUserRepository storeUserRepository)
        {
            _storeUserRepository = storeUserRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _storeUserRepository.InsertAsync(new StoreUser
            (
                id: Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"),
                desc: "cd10d72d59684cb49a0ed074bb9d2e7519686bb657d4422e9b9258394eb24e49dce88e67b6d94e3da11189d",
                isActive: true
            ));

            await _storeUserRepository.InsertAsync(new StoreUser
            (
                id: Guid.Parse("b7770553-fd57-44e7-b1c8-1b67739fc8e4"),
                desc: "d047b15fa5144099a2521e1b477207fa",
                isActive: true
            ));
        }
    }
}