using BYBY.Infrastructure;
using BYBY.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Cache
{
    public interface ICacheService
    {
        Task<IList<SelectItem>> GetSelectItemAsync(CacheKeys key);

      //  void SaveRoleModulesToCache(TBUser user, string roleName);
    }
}
