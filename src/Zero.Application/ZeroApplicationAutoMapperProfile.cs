using Zero.Users;
using Volo.Abp.Identity;
using Zero.StoreUsers;
using System;
using Zero.Shared;
using Volo.Abp.AutoMapper;
using Zero.Stores;
using AutoMapper;

namespace Zero
{
    public class ZeroApplicationAutoMapperProfile : Profile
    {
        public ZeroApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<StoreCreateDto, Store>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<StoreUpdateDto, Store>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<Store, StoreDto>();
            CreateMap<StoreUserCreateDto, StoreUser>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<StoreUserUpdateDto, StoreUser>().IgnoreFullAuditedObjectProperties().Ignore(x => x.ExtraProperties).Ignore(x => x.ConcurrencyStamp).Ignore(x => x.Id);
            CreateMap<StoreUser, StoreUserDto>();
            CreateMap<StoreUserWithNavigationProperties, StoreUserWithNavigationPropertiesDto>();
            CreateMap<Store, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

            CreateMap<AppUser, AppUserDto>().IgnoreExtraProperties().Ignore(x => x.ExtraProperties);
            CreateMap<IdentityUser, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Email));
            CreateMap<AppUser, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Email));
        }
    }
}