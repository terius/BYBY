using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IHospitalService : IBaseService
    {
        /// <summary>
        /// 查询会诊室
        /// </summary>
        /// <returns></returns>
        Task<IList<ConsultationRoomListView>> GetRoomList();

        /// <summary>
        /// 新增会诊室
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<EmptyResponse> AddRoom(AddRoomRequest request);


        /// <summary>
        /// 编辑会诊室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> EditRoom(ConsultationRoomListView request);


        /// <summary>
        ///  删除会诊室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteRoom(OnlyHasIdRequest request);


        Task<IList<DateSetupListView>> GetDateSetupList();
        Task<EmptyResponse> AddDateSetup(AddDateSetupRequest request);
        Task<EmptyResponse> DeleteDateSetup(OnlyHasIdRequest request);

        Task<PlanListView> GetPlanList(PlanQueryRequest request);
    }
}
