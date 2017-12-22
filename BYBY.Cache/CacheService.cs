﻿using BYBY.Cache.CacheStorage;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace BYBY.Cache
{
    public class CacheService : ICacheService
    {
        ICacheStorage _cacheStorage;
        readonly IRepository<TBNation, int> _nationRepository;
        readonly IRepository<TBJob, int> _jobRepository;
        readonly IRepository<TBEthnic, int> _ethnicRepository;
        readonly IRepository<TBHospital, int> _hospitalRepository;
        readonly IRepository<TBDoctor, int> _doctorRepository;
        readonly object NationObj = new object();

        public CacheService(ICacheStorage cacheStorage,
            IRepository<TBNation, int> nationRepository,
            IRepository<TBJob, int> jobRepository,
            IRepository<TBEthnic, int> ethnicRepository,
            IRepository<TBHospital, int> hospitalRepository,
            IRepository<TBDoctor, int> doctorRepository
            )
        {
            _cacheStorage = cacheStorage;
            _nationRepository = nationRepository;
            _jobRepository = jobRepository;
            _ethnicRepository = ethnicRepository;
            _hospitalRepository = hospitalRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IList<SelectItem>> GetSelectItemAsync(CacheKeys key)
        {
            IList<SelectItem> cacheData = _cacheStorage.Retrieve<IList<SelectItem>>(key.ToString());
            if (cacheData == null)
            {
                switch (key)
                {
                    case CacheKeys.Nation:
                        var nationData = await _nationRepository.FindAllAsync();
                        cacheData = nationData.ConvertTo_SelectItem();
                        break;
                    case CacheKeys.Job:
                        var jobData = await _jobRepository.FindAllAsync();
                        cacheData = jobData.ConvertTo_SelectItem();
                        break;
                    case CacheKeys.Ethnic:
                        var ethnicData = await _ethnicRepository.FindAllAsync();
                        cacheData = ethnicData.ConvertTo_SelectItem();
                        break;
                    case CacheKeys.Education:
                        cacheData = CreateEnumList(typeof(Education));
                        break;
                    case CacheKeys.CardType:
                        cacheData = CreateEnumList(typeof(CardType));
                        break;
                    case CacheKeys.Marriage:
                        cacheData = CreateEnumList(typeof(MaritalStatus));
                        break;
                    case CacheKeys.Hospital:
                        var hospitalData = await _hospitalRepository.FindAllAsync();
                        cacheData = hospitalData.ConvertTo_SelectItem();
                        break;
                    case CacheKeys.Doctor:
                        var doctorData = await _doctorRepository.FindAllAsync();
                        cacheData = doctorData.ConvertTo_SelectItem();
                        break;
                    default:
                        break;
                }
                _cacheStorage.Store(key.ToString(), cacheData);

            }
            return cacheData;
        }


     

        private IList<SelectItem> CreateEnumList(Type type, bool ValueIsInt = true)
        {
            IList<SelectItem> list = new List<SelectItem>();
            string strName, strVaule, strTitle;
            foreach (var myCode in Enum.GetValues(type))
            {

                FieldInfo field = type.GetField(myCode.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                strName = attributes[0].Description;
                strVaule = ValueIsInt ? ((int)myCode).ToString() : myCode.ToString();//获取值  
                strTitle = ValueIsInt ? myCode.ToString() : ((int)myCode).ToString();
                SelectItem myLi = new SelectItem { id = strVaule, text = strName, title = strTitle };
                list.Add(myLi);
            }
            return list;
        }

        //public void SaveRoleModulesToCache(TBUser user, string roleName)
        //{
        //    var Modules = user.GetModules(roleName);
        //    _cacheStorage.Remove(CacheKeys.RoleModule.ToString());
        //    _cacheStorage.Store(CacheKeys.RoleModule.ToString(), Modules);
        //}

       
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
        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBEthnic> source)
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

        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBHospital> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.Name;
                sitem.title = item.IsMaster ? "1" : "0";
                dest.Add(sitem);
            }
            return dest;
        }

        public static IList<SelectItem> ConvertTo_SelectItem(this IEnumerable<TBDoctor> source)
        {
            var dest = new List<SelectItem>();
            SelectItem sitem;
            foreach (var item in source)
            {
                sitem = new SelectItem();
                sitem.id = item.Id.ToString();
                sitem.text = item.User.Name;
                sitem.title = item.JobTitle;
                dest.Add(sitem);
            }
            return dest;
        }

    }
}
