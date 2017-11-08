using BYBY.Services.Request;
using BYBY.Services.Response;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IUserAccountService
    {
        IdentityResult CreateUser(UserRegRequest request);

        Task<EmptyResponse> UserLogin(UserLoginRequest request);
    }
}
