using BYBY.Repository.Entities;
using BYBY.Services.Request;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sceneray.CSCenter.AppService.SSUser
{
    public class UserManager : UserManager<TBUser, int>
    {
        private const string defaultPwd = "changeme/123";
        //  readonly RoleManager _roleManager;
        public UserManager(UserStore store) : base(store)
        {
            UserStore = store;
            //var provider = new DpapiDataProtectionProvider("SSC20");
            //UserTokenProvider = new DataProtectorTokenProvider<TBUser,int>(
            //  provider.Create("UserToken"));
            PasswordValidator = new PasswordValidator { RequiredLength = 1 };
            //_roleManager = roleManager;
        }
        protected UserStore UserStore { get; set; }


        public IdentityResult CreateUser(UserRegRequest request, out int newUserId)
        {

            var user = new TBUser();
            //user.Name = user.Surname = request.Name;
            //user.UserName = request.LoginUserName;
            //user.Password = new PasswordHasher().HashPassword(defaultPwd);
            //user.EmailAddress = request.LoginUserName + "@sceneray.com";
            //user.IsEmailConfirmed = true;
            //user.IsActive = true;
            //user.IsDeleted = false;
            //user.CreationTime = DateTime.Now;
            //if (request.CurrUserId > 0)
            //{
            //    user.CreatorUserId = request.CurrUserId;
            //}
            //user.AccessFailedCount = 0;
            //user.IsLockoutEnabled = true;
            //user.IsPhoneNumberConfirmed = false;
            //user.IsTwoFactorEnabled = false;
            //user.SecurityStamp = Guid.NewGuid().ToString();
            var result = this.Create(user);
            newUserId = user.Id;
            return result;

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
