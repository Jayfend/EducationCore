using EducationCore.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Contracts.Services
{
    public interface IUserService
    {
        public Task<bool> CreateAsync(UserCreateDTO req);

        public Task<UserInfoDTO> GetAsync(string userName);
    }
}