using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BYBY.Services.Account
{
    public class ApplicationUser : TBUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TBUser,int> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}
