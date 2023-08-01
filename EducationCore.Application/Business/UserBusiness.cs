using AutoMapper;
using EducationCore.Application.Contracts.Business;
using EducationCore.Application.Contracts.DTO;
using EducationCore.Application.Contracts.Exceptions;
using EducationCore.Data.Entities;
using EducationCore.Data.Entity_Framework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserBusiness(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(UserCreateDTO req)
        {
            var userByEmail = await _userManager.FindByEmailAsync(req.Email);
            var userByName = await _userManager.FindByNameAsync(req.UserName);
            if (userByEmail != null || userByName != null)
            {
                throw new ExceptionHandler(ExceptionCodes.ERROR_EDCORE_ITEM_EXIST, "Tài khoản đã tồn tại");
            }
            var newUser = _mapper.Map<UserCreateDTO, User>(req);
            var result = await _userManager.CreateAsync(newUser, req.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserInfoDTO> GetAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName.Trim());
            if (user == null)
            {
                throw new ExceptionHandler(ExceptionCodes.ERROR_EDCORE_NOT_FOUND, "Tài khoản không tồn tại");
            }
            return _mapper.Map<User, UserInfoDTO>(user);
        }
    }
}