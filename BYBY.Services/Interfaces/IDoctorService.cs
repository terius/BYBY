using BYBY.Infrastructure;
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

        /// <summary>
        /// 获取当前登录医生所属医院下面的所有医生
        /// </summary>
        /// <returns></returns>
        Task<IList<SelectItem>> GetDoctorListByHospital();



    }
}
