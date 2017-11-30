using BYBY.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Cache
{
    public interface ICacheService
    {
        Task<IList<SelectItem>> GetSelectItemAsync(CacheKeys key);
    }
}
