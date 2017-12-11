using BYBY.Repository.Entities;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IMedicalHistoryService
    {
        /// <summary>
        /// 获取病历列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<MedicalHistoryListView>> GetMedicalHistoryList(MedicalHistoryListSearchRequest request);

        /// <summary>
        /// 新增病历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveAdd(MedicalHistoryAddRequest request);


        /// <summary>
        /// 获取病历编辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MedicalHistoryEditRequest> GetEditData(int id);


        /// <summary>
        /// 保存病历编辑信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveEditBaseInfo(MedicalHistoryEditRequest request);


        /// <summary>
        /// 获取女方病历
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        Task<IList<MedicalDetailRequest>> GetMedicalDetails(TBPatient patient);
    }
}
