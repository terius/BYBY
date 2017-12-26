using AutoMapper;
using BYBY.Cache;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Services.Implementations
{
    public class SystemService : BaseService, ISystemService
    {

        readonly IRepository<TBMedicine, int> _medicineRepository;
        readonly IRepository<TBCheckAssay, int> _checkRepository;
        readonly IUnitOfWork _unitOfWork;
        readonly ICacheService _cacheService;

        public SystemService(IRepository<TBMedicine, int> medicineRepository,
            IRepository<TBCheckAssay, int> checkRepository,
            ICacheService cacheService,
        IUnitOfWork unitOfWork)
        {
            _medicineRepository = medicineRepository;
            _checkRepository = checkRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<PagedData<MedicineListView>> GetMedicineList(MedicineQueryRequest request)
        {
            var query = _medicineRepository.GetDbQuerySet();
            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => Mapper.Map<List<MedicineListView>>(d));
            return await Task.FromResult(pageData);
        }



        /// <summary>
        /// 新增药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddMedicine(MedicineAddRequest request)
        {
            var info = Mapper.Map<TBMedicine>(request);
            info.AddUserName = GetLoginUserName();
            await _medicineRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs>0)
            {
                _cacheService.RemoveCache(CacheKeys.Medicine);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        /// <summary>
        /// 编辑药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> EditMedicine(MedicineEditRequest request)
        {
            var editInfo = await _medicineRepository.GetAsync(request.Id);
            editInfo = Mapper.Map(request, editInfo);
            editInfo.ModifyUserName = GetLoginUserName();
            await _medicineRepository.UpdateAsync(editInfo);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Medicine);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("编辑成功") : EmptyResponse.CreateError("编辑失败");
        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteMedicine(OnlyHasIdRequest request)
        {
            var info = await _medicineRepository.GetAsync(request.Id);
            await _medicineRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Medicine);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }




        /// <summary>
        /// 查询检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedData<CheckListView>> GetCheckList(CheckQueryRequest request)
        {
            var query = _checkRepository.GetDbQuerySet();
            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => Mapper.Map<List<CheckListView>>(d));
            return await Task.FromResult(pageData);
        }

        
        /// <summary>
        /// 新增检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddCheck(CheckListView request)
        {
            var info = Mapper.Map<TBCheckAssay>(request);
            info.AddUserName = GetLoginUserName();
            await _checkRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.CheckAssay);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        /// <summary>
        /// 编辑检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> EditCheck(CheckListView request)
        {
            var editInfo = await _checkRepository.GetAsync(request.Id);
            editInfo = Mapper.Map(request, editInfo);
            editInfo.ModifyUserName = GetLoginUserName();
            await _checkRepository.UpdateAsync(editInfo);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.CheckAssay);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("编辑成功") : EmptyResponse.CreateError("编辑失败");
        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteCheck(OnlyHasIdRequest request)
        {
            var info = await _checkRepository.GetAsync(request.Id);
            await _checkRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.CheckAssay);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }

    }
}
