using EducationCore.Application.Contracts.Business;
using EducationCore.Application.Contracts.DTO;
using EducationCore.Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserBusiness _userBusiness;

        public UserService(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        public async Task<bool> CreateAsync(UserCreateDTO req)
        {
            return await _userBusiness.CreateAsync(req);
        }

        public async Task<UserInfoDTO> GetAsync(string userName)
        {
            return await _userBusiness.GetAsync(userName);
        }
    }
}