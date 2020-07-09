using Zero.StoreUsers;
using Zero.Stores;
using AutoMapper;

namespace Zero.Web
{
    public class ZeroWebAutoMapperProfile : Profile
    {
        public ZeroWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.

            CreateMap<StoreDto, StoreUpdateDto>();

            CreateMap<StoreUserDto, StoreUserUpdateDto>();
        }
    }
}