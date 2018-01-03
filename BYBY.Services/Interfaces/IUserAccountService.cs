using BYBY.Services.Request;
using BYBY.Services.Response;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<EmptyResponse> CreateUserAsync(UserCreateRequest request);

        Task<EmptyResponse> UserLogin(UserLoginRequest request);

        Task<string> CreateUserReturnUserId(UserCreateRequest request);

        Task UpdateUserNameAsync(string userId, string newUserName);

        Task<IdentityResult> DeleteUserAsync(string userId);
    }
}
