using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zero.StoreUsers;
using Zero.EntityFrameworkCore;
using Xunit;

namespace Zero.StoreUsers
{
    public class StoreUserRepositoryTests : ZeroEntityFrameworkCoreTestBase
    {
        private readonly IStoreUserRepository _storeUserRepository;

        public StoreUserRepositoryTests()
        {
            _storeUserRepository = GetRequiredService<IStoreUserRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _storeUserRepository.GetListAsync(
                    desc: "cd10d72d59684cb49a0ed074bb9d2e7519686bb657d4422e9b9258394eb24e49dce88e67b6d94e3da11189d",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _storeUserRepository.GetCountAsync(
                    desc: "d047b15fa5144099a2521e1b477207fa",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}