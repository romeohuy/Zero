using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Zero.Stores
{
    public class StoreAppServiceTests : ZeroApplicationTestBase
    {
        private readonly IStoreAppService _storeAppService;
        private readonly IRepository<Store, Guid> _storeRepository;

        public StoreAppServiceTests()
        {
            _storeAppService = GetRequiredService<IStoreAppService>();
            _storeRepository = GetRequiredService<IRepository<Store, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _storeAppService.GetListAsync(new GetStoresInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("723dabc2-a59f-45ae-b608-a55aa04736d3")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _storeAppService.GetAsync(Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new StoreCreateDto
            {
                Name = "89357d2464f94888aa223740c993b827bda075cf50e14355982482bbd0a22e6aebb06dd1f85c497e9bb576401",
                Url = "c5737c7bcbd14165a0189e",
                IsActive = true
            };

            // Act
            var serviceResult = await _storeAppService.CreateAsync(input);

            // Assert
            var result = await _storeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("89357d2464f94888aa223740c993b827bda075cf50e14355982482bbd0a22e6aebb06dd1f85c497e9bb576401");
            result.Url.ShouldBe("c5737c7bcbd14165a0189e");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new StoreUpdateDto()
            {
                Name = "bea06ff4f4974179a7b8dee7dfac7675d7679a86a7794262b1ed9cc5c35d7719b8ec9471c02f406eaea6f2ad3d5",
                Url = "c3fda4b4b7d94cd08f6dc148db68ddc57323145a9f5a4c",
                IsActive = true
            };

            // Act
            var serviceResult = await _storeAppService.UpdateAsync(Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"), input);

            // Assert
            var result = await _storeRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("bea06ff4f4974179a7b8dee7dfac7675d7679a86a7794262b1ed9cc5c35d7719b8ec9471c02f406eaea6f2ad3d5");
            result.Url.ShouldBe("c3fda4b4b7d94cd08f6dc148db68ddc57323145a9f5a4c");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _storeAppService.DeleteAsync(Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"));

            // Assert
            var result = await _storeRepository.FindAsync(c => c.Id == Guid.Parse("fe6706ea-f842-403f-af02-73fae30259c0"));

            result.ShouldBeNull();
        }
    }
}