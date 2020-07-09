using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Zero.StoreUsers
{
    public class StoreUserAppServiceTests : ZeroApplicationTestBase
    {
        private readonly IStoreUserAppService _storeUserAppService;
        private readonly IRepository<StoreUser, Guid> _storeUserRepository;

        public StoreUserAppServiceTests()
        {
            _storeUserAppService = GetRequiredService<IStoreUserAppService>();
            _storeUserRepository = GetRequiredService<IRepository<StoreUser, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _storeUserAppService.GetListAsync(new GetStoreUsersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.StoreUser.Id == Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c")).ShouldBe(true);
            result.Items.Any(x => x.StoreUser.Id == Guid.Parse("b7770553-fd57-44e7-b1c8-1b67739fc8e4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _storeUserAppService.GetAsync(Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new StoreUserCreateDto
            {
                Desc = "a021763174504529b1134894d1eb5f5420f69ed3d4234c04b281d7978523d",
                IsActive = true
            };

            // Act
            var serviceResult = await _storeUserAppService.CreateAsync(input);

            // Assert
            var result = await _storeUserRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Desc.ShouldBe("a021763174504529b1134894d1eb5f5420f69ed3d4234c04b281d7978523d");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new StoreUserUpdateDto()
            {
                Desc = "7128faeea1d742ccae3a0c962510cace6578955bf7e544dfbdb20e3d7119",
                IsActive = true
            };

            // Act
            var serviceResult = await _storeUserAppService.UpdateAsync(Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"), input);

            // Assert
            var result = await _storeUserRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Desc.ShouldBe("7128faeea1d742ccae3a0c962510cace6578955bf7e544dfbdb20e3d7119");
            result.IsActive.ShouldBe(true);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _storeUserAppService.DeleteAsync(Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"));

            // Assert
            var result = await _storeUserRepository.FindAsync(c => c.Id == Guid.Parse("6a83d261-4afe-4de3-b061-6875fd61395c"));

            result.ShouldBeNull();
        }
    }
}