using BYBY.Repository.Entities;
using BYBY.Services.Request;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Account
{
    public class UserManager : UserManager<TBUser, string>
    {
        private const string defaultPwd = "123456";
        //   readonly RoleManager _roleManager;
        public UserManager(UserStore store) : base(store)
        {

            UserStore = store;
            //var provider = new DpapiDataProtectionProvider("SSC20");
            //UserTokenProvider = new DataProtectorTokenProvider<TBUser,int>(
            //  provider.Create("UserToken"));
            PasswordValidator = new PasswordValidator { RequiredLength = 1 };
            //  _roleManager = roleManager;
        }
        protected UserStore UserStore { get; set; }


        public async Task<IdentityResult> CreateUserAsync(UserCreateRequest request)
        {
            if (await CheckUserNameExist(request.UserName))
            {
                throw new Exception("该用户名已存在，无法创建");
            }
            var user = new TBUser();
            user.Password = new PasswordHasher().HashPassword(defaultPwd);
            user.UserName = request.UserName;
            user.Name = request.Name;
            user.HospitalId = request.HospitalId;
            //  user.Id = Guid.NewGuid().ToString();
            var result = await CreateAsync(user);
            // newUserId = user.Id;
            if (result.Succeeded)
            {
                await AddToRoleAsync(user.Id, request.RoleName);
            }
            return result;
        }

        private async Task<bool> CheckUserNameExist(string userName, string userId = null)
        {

            var info = await FindByNameAsync(userName);
            if (userId == null)
            {
                return info != null;
            }
            if (info != null && info.Id != userId)
            {
                return true;
            }
            return false;
        }




        public void UpdateUserTrueName(string userName, string newTrueName)
        {
            var userInfo = this.FindByName(userName);
            if (userInfo != null && !userInfo.Name.Equals(newTrueName))
            {
                userInfo.Name = newTrueName;
                var result = this.Update(userInfo);
            }
        }

        public async Task UpdateUserNameAsync(string userId, string newUserName)
        {
            if (await CheckUserNameExist(newUserName,userId))
            {
                throw new Exception("该用户名已存在，无法编辑");
            }
            var userInfo = await FindByIdAsync(userId);
            if (userInfo != null && !userInfo.UserName.Equals(newUserName))
            {
                userInfo.UserName = newUserName;
                await UpdateAsync(userInfo);
            }
        }

        public async Task UpdateUserNameAndHospitalAsync(string userId, string newUserName, int hospitalId)
        {
            if (await CheckUserNameExist(newUserName, userId))
            {
                throw new Exception("该用户名已存在，无法编辑");
            }
            var userInfo = await FindByIdAsync(userId);
            if (userInfo != null)
            {
                userInfo.UserName = newUserName;
                userInfo.HospitalId = hospitalId;
                await UpdateAsync(userInfo);
            }
        }



        public bool CheckPasswordByUserName(string userName, string password)
        {
            var user = this.FindByName(userName);// await UserStore.FindByNameAsync(userName);
            return this.CheckPassword(user, password);
        }

        public IdentityResult DeleteByUserName(string userName)
        {
            var user = this.FindByName(userName);
            return this.Delete(user);
        }

        public async Task<IdentityResult> DeleteByUserIdAsync(string userId)
        {
            var user = await FindByIdAsync(userId);
            return await DeleteAsync(user);
        }

        public async Task<IEnumerable<TBUser>> GetAllUsers()
        {
            return await UserStore.FindAllUser();

        }



        public IdentityResult ResetPassword(string userName)
        {
            var user = this.FindByName(userName);
            string resetToken = this.GeneratePasswordResetToken(user.Id);
            IdentityResult passwordChangeResult = this.ResetPassword(user.Id, resetToken, defaultPwd);
            return passwordChangeResult;
        }

        //public string GetRoleDisplayName(string roleName)
        //{
        //    var role = _roleManager.FindByName(roleName);
        //    return role.DisplayName;
        //}

    }



}
