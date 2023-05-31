using AutoMapper;
using UserManagment.API.DTO.UserProfileDTO;
using UserManagment.Core.Entities;

namespace UserManagment.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();

            CreateMap<UserProfile, UserProfileCreateDTO>().ReverseMap();

            CreateMap<UserProfile, UserProfileUpdateDTO>().ReverseMap();

            CreateMap<UserProfile, UserProfileDetailDTO>()
            .ForMember(u => u.UserName, m => m.MapFrom(p => p.User.UserName))
            .ForMember(u => u.Email, m => m.MapFrom(p => p.User.Email))
            .ForMember(u => u.IsActive, m => m.MapFrom(p => p.User.IsActive))
            .ReverseMap();
        }
    }
}
