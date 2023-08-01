using AutoMapper;
using EducationCore.Application.Contracts.DTO;
using EducationCore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<User, UserInfoDTO>().ReverseMap();
        }
    }
}