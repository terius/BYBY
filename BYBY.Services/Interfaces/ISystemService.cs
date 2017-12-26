using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface ISystemService: IBaseService
    {
        /// <summary>
        /// 查询药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<MedicineListView>> GetMedicineList(MedicineQueryRequest request);


        /// <summary>
        /// 新增药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> AddMedicine(MedicineAddRequest request);

        /// <summary>
        /// 编辑药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> EditMedicine(MedicineEditRequest request);

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteMedicine(OnlyHasIdRequest request);



        /// <summary>
        /// 查询检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedData<CheckListView>> GetCheckList(CheckQueryRequest request);



        /// <summary>
        /// 新增检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> AddCheck(CheckListView request);


        /// <summary>
        /// 编辑检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> EditCheck(CheckListView request);


        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EmptyResponse> DeleteCheck(OnlyHasIdRequest request);
    }
}
