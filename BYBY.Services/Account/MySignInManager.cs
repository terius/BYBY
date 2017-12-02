using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Account
{
    /// <summary>
    /// 登录管理，此处用到了UserManager
    /// </summary>
    public class MySignInManager : SignInManager<TBUser, int>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="UserManager"></param>
        /// <param name="AuthenticationManager"></param>
        public MySignInManager(UserManager UserManager, Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager)
            : base(UserManager, AuthenticationManager)
        {

        }

        /// <summary>
        /// 根据用户名密码，验证用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        public override Task<SignInStatus> PasswordSignInAsync(string userName,string password,bool isPersistent,bool shouldLockout)
        {
            /*这里可以直接通过PasswordSignInAsync来校验，也可以重写~ */
            //这里用Find方法会返回空的user。。。搞不懂。。
            var user = base.UserManager.FindByName(userName);
            if (user == null || user.Id < 1)
                return Task.FromResult(SignInStatus.Failure);
            else if (user.Password != password)
                return Task.FromResult(SignInStatus.Failure);
            else
            {
                /*这个时候如果不写入到cooks里，在Home控制器的Index action里会被系统的
                    Authorize刷选器拦截*/
                // 利用ASP.NET Identity获取identity 对象
                var identity = base.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                // 将上面拿到的identity对象登录
                base.AuthenticationManager.SignIn(new AuthenticationProperties()
                { IsPersistent = true }, identity.Result);
                return Task.FromResult(SignInStatus.Success);
            }
            /*这里如果你想直接使用微软的登入方法也可以，直接base.就ok啦*/
            //return base.PasswordSignInAsync(userName,
            //                                password,
            //                                isPersistent,
            //                                shouldLockout);
        }

    }
}
