using BYBY.Infrastructure;
using BYBY.Repository.Entities;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IDoctorService : IBaseService
    {
       /// <summary>
       /// 获取母院医生所有子院列表
       /// </summary>
       /// <returns></returns>
        Task<IList<SelectItem>> GetDoctorChildHospitals();


        /// <summary>
        /// 获取所有的母院和子院（分级显示）
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectItem>> GetGroupHospitals();



    }
}
