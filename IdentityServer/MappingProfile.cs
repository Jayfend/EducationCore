using AutoMapper;
using IdentityServer.DTO;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, IdentityUser>().ReverseMap();
        }
    }
}