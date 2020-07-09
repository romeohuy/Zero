using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zero.Stores;
using Zero.EntityFrameworkCore;
using Xunit;

namespace Zero.Stores
{
    public class StoreRepositoryTests : ZeroEntityFrameworkCoreTestBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreRepositoryTests()
        {
            _storeRepository = GetRequiredService<IStoreRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _storeRepository.GetListAsync(
                    name: "8c62f167554a472facb722924f48f3c06941f5547c09460d9a06bc614d79b8f0d24fa5ca0",
                    url: "169189c2a39a413fa74bddd6ee0f5cc5db9527089823401284f6b2e30ad9ad55348c095996fe476187d42b792b8189",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _storeRepository.GetCountAsync(
                    name: "49f68f4383614b6a9d4015a5eec22782d11e3236",
                    url: "532003024f0c4ff9af860096a9fdf2e",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}