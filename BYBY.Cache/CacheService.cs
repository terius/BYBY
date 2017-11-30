using BYBY.Cache.CacheStorage;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Cache
{
    public class CacheService : ICacheService
    {
        ICacheStorage _cacheStorage;
        readonly IRepository<TBNation, int> _nationRepository;
        readonly IRepository<TBJob, int> _jobRepository;
        readonly object NationObj = new object();

        public CacheService(ICacheStorage cacheStorage, 
            IRepository<TBNation, int> nationRepository,
            IRepository<TBJob, int> jobRepository
            )
        {
            _cacheStorage = cacheStorage;
            _nationRepository = nationRepository;
            _jobRepository = jobRepository;
        }

        public async Task<IList<SelectItem>> GetSelectItemAsync(CacheKeys key)
        {
            IList<SelectItem> cacheData = null;
            switch (key)
            {
                case CacheKeys.Nation:
                    cacheData = _cacheStorage.Retrieve<IList<SelectItem>>(CacheKeys.Nation.ToString());
                    if (cacheData == null)
                    {
                        var nationData = await _nationRepository.FindAllAsync();
                        cacheData = nationData.ConvertTo_SelectItem();
                    }
                    break;
                case CacheKeys.Job:
                    cacheData = _cacheStorage.Retrieve<IList<SelectItem>>(CacheKeys.Job.ToString());
                    if (cacheData == null)
                    {
                        var jobData = await _jobRepository.FindAllAsync();
                        cacheData = jobData.ConvertTo_SelectItem();
                    }
                    break;
                default:
                    break;
            }

            return cacheData;
        }
    }

    public static class ConvertExt
    {
        //terius del
        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBNation> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.Chinese;
                sitem.title = item.English;
                dest.Add(sitem);
            }
            return dest;
        }

        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBJob> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.Name;
                dest.Add(sitem);
            }
            return dest;
        }

    }
}
