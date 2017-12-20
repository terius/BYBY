using BYBY.Repository.Entities;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IMedicalHistoryService : IBaseService
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
        Task<MedicalHistoryDetailModel> GetDetailModel(int id);


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

        /// <summary>
        /// 保存病历信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveEditMedicalDetail(MedicalDetailRequest request);

        /// <summary>
        /// 保存申请会诊信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveConsultationAdd(ConsultationAddRequest request);

        /// <summary>
        /// 保存申请转诊信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveReferralAdd(ReferralAddRequest request);

        /// <summary>
        /// 取消会诊
        /// </summary>
        /// <param name="consultationId"></param>
        /// <returns></returns>
        Task<EmptyResponse> UpdateConsultationToCancel(ConsultationCancelRequest cancelRequest);

        /// <summary>
        /// 取消转诊
        /// </summary>
        /// <param name="cancelRequest"></param>
        /// <returns></returns>
        Task<EmptyResponse> UpdateReferralToCancel(ReferralCancelRequest cancelRequest);

        /// <summary>
        /// 删除病历信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> Delete(MedicalHistoryDeleteRequest request);


        /// <summary>
        /// 查询会诊信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<ConsultationListView>> GetConsultationList(ConsultationQueryRequest request);

        /// <summary>
        /// 查询转诊信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<ReferralListView>> GetReferralList(ReferralQueryRequest request);
    }
}
