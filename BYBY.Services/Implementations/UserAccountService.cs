using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using Microsoft.AspNet.Identity;

namespace BYBY.Services.Implementations
{
    public class UserAccountService :IUserAccountService
    {
        private readonly UserManager _userManager = UserFactory.GetUserManager();
        public UserAccountService()
        {

        }

        public IdentityResult CreateUser(UserRegRequest request)
        {
            return _userManager.CreateUser(request, out int newId);
        }
    }
}
