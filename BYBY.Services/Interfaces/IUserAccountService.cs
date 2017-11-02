using BYBY.Services.Request;
using Microsoft.AspNet.Identity;

namespace BYBY.Services.Interfaces
{
    public interface IUserAccountService
    {
        IdentityResult CreateUser(UserRegRequest request);
    }
}
