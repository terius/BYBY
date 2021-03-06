﻿using BYBY.Repository.Entities;
using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace BYBY.Services.Implementations
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        readonly UserManager _userManager = UserFactory.GetUserManager();
        public UserAccountService()
        {

        }

        public async Task<EmptyResponse> CreateUserAsync(UserCreateRequest request)
        {
            var response = new EmptyResponse();
            var result = await _userManager.CreateUserAsync(request);
            if (result.Succeeded)
            {
                response.CreateSuccessResponse();
            }
            else
            {
                string errors = string.Join(",", result.Errors);
                response.CreateErrorResponse(errors);
            }
            return response;
        }

        public async Task<string> CreateUserReturnUserId(UserCreateRequest request)
        {
            var res = await CreateUserAsync(request);
            if (res.Result)
            {
                var userInfo = await _userManager.FindByNameAsync(request.UserName);
                return userInfo.Id;
            }
            return null;
        }

        public async Task<EmptyResponse> UserLogin(UserLoginRequest request)
        {
            var response = new EmptyResponse();
            var userInfo = await _userManager.FindByNameAsync(request.UserName);
            if (userInfo != null)
            {
                var verificationResult = _userManager.PasswordHasher.VerifyHashedPassword(userInfo.Password, request.Password);
                if (verificationResult == PasswordVerificationResult.Success)
                {
                    if (CheckUserRole(userInfo, request.RoleId))
                    {
                        response.CreateSuccessResponse();
                    }
                    else
                    {
                        response.CreateErrorResponse("登录角色错误");
                    }

                }
                else
                {
                    response.CreateErrorResponse("密码错误");
                }
            }
            else
            {
                response.CreateErrorResponse("用户名错误");
            }
            return response;

        }

        private bool CheckUserRole(TBUser user, string roleId)
        {
            foreach (var userRole in user.UserRoles)
            {
                if (userRole.RoleId == roleId)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task UpdateUserNameAsync(string userId, string newUserName)
        {
            await _userManager.UpdateUserNameAsync(userId, newUserName);

        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            return await _userManager.DeleteByUserIdAsync(userId);

        }
    }
}
