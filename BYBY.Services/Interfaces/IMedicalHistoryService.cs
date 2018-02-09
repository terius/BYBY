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
        /// 编辑病历详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveEditMedicalDetail(MedicalDetailRequest request);

        /// <summary>
        /// 新增病历详细信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveAddMedicalDetail(MedicalDetailAddRequest request);

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

        /// <summary>
        /// 保存病历图片
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="MHId"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveMHImage(IList<string> filePaths, int MHId);


        /// <summary>
        /// 删除病历附件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteImage(MedicalHistoryImageDeleteRequest request);

        /// <summary>
        /// 会诊详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ConsultationDetailModel> GetConsultationDetail(int id);

        /// <summary>
        /// 保存会诊记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> SaveConsultationRecord(ConsultationRecordEditRequest request);


        /// <summary>
        /// 更改会诊状态
        /// </summary>
        /// <param name="consultationId"></param>
        /// <returns></returns>
        Task<EmptyResponse> UpdateConsultationStatus(UpdateConsultationStatusRequest request);


        /// <summary>
        /// 新增药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> AddMedicine(ConsultationMedicineListRequest request);


        /// <summary>
        /// 修改药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> EditMedicine(ConsultationMedicineListRequest request);

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteMedicine(OnlyHasIdRequest request);


        /// <summary>
        /// 新增检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> AddCheck(ConsultationCheckListRequest request);


        /// <summary>
        /// 修改检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> EditCheck(ConsultationCheckListRequest request);


        /// <summary>
        /// 删除检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteCheck(OnlyHasIdRequest request);


        /// <summary>
        /// 更改会诊状态
        /// </summary>
        /// <param name="consultationId"></param>
        /// <returns></returns>
        Task<EmptyResponse> UpdateReferralStatus(UpdateReferralStatusRequest request);

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        Task<MainModel> GetMainModel();

        Task<IList<DisplayModel>> GetReferralDetail(OnlyHasIdRequest request);

        Task<ReportView> GetReport(ReportQueryRequest request);

        Task<PagedData<ReportListView>> GetConsultationListInReport(ReportQueryRequest request);

        Task<PagedData<ReportListView>> GetReferralListInReport(ReportQueryRequest request);

        Task<FemalePrintResponse> GetFemalePrint(PrintMDRequest request);

        Task<MalePrintResponse> GetMalePrint(PrintMDRequest request);
    }
}
