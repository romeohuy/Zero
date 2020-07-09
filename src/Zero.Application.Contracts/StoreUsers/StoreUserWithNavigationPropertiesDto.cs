using Zero.Stores;
using Zero.Users;

using System;
using Volo.Abp.Application.Dtos;

namespace Zero.StoreUsers
{
    public class StoreUserWithNavigationPropertiesDto
    {
        public StoreUserDto StoreUser { get; set; }

        public StoreDto Store { get; set; }
        public AppUserDto AppUser { get; set; }

    }
}