using BYBY.Services.Account;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using BYBY.Services;


namespace BYBY.Services.Implementations
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        private readonly UserManager _userManager = UserFactory.GetUserManager();
        public UserAccountService()
        {

        }

        public IdentityResult CreateUser(UserRegRequest request)
        {
            return _userManager.CreateUser(request, out int newId);
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
                    response.CreateSuccessResponse();
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
    }
}
