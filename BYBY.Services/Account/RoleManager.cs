using BYBY.Repository.Entities;
using Microsoft.AspNet.Identity;

namespace BYBY.Services.Account
{
    public class RoleManager : RoleManager<TBRole, int>
    {
        public RoleStore RoleStore { get; set; }
        public RoleManager(RoleStore store) : base(store)
        {
           
            RoleStore = store;
        }
    }
}
