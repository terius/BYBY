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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BYBY.Services.Implementations
{
    public class DoctorService : BaseService, IDoctorService
    {

        readonly IRepository<TBDoctor, int> _repository;
        readonly IRepository<TBHospital, int> _hospitalRepository;
        readonly IUnitOfWork _unitOfWork;
        readonly ICacheService _cacheService;
        readonly IUserAccountService _userService;

        public DoctorService(IRepository<TBDoctor, int> repository,
            IRepository<TBHospital, int> hospitalRepository,
            ICacheService cacheService,
            IUserAccountService userService,
        IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _hospitalRepository = hospitalRepository;
            _cacheService = cacheService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SelectItem>> GetDoctorChildHospitals()
        {
            var doctor = await GetLoginDoctorInfo();
            if (doctor.IsMasterDoctor)
            {
                var hospitalId = doctor.Hospital.Id;
                var list = await _hospitalRepository.FindAsync(d => d.ParentHospitalId == hospitalId);
                return list.ConvertTo_SelectItem();
            }
            return new List<SelectItem>();
        }

        public async Task<IList<SelectItem>> GetDoctorListByHospital()
        {
            var doctor = await GetLoginDoctorInfo();
            var list = doctor.Hospital.Doctors.ConvertTo_SelectItem();
            return list;
        }

        public async Task<IList<SelectItem>> GetGroupHospitals()
        {
            IList<SelectItem> pList = new List<SelectItem>();
            var masterList = await _hospitalRepository.FindAsync(d => d.IsMaster == true);
            SelectItem hosp;
            foreach (var item in masterList)
            {
                hosp = new SelectItem { id = item.Id.ToString(), text = item.Name + "-母院" };
                var childList = await _hospitalRepository.FindAsync(d => d.ParentHospitalId == item.Id);
                hosp.children = childList.ConvertTo_SelectItem();
                hosp.children.Insert(0, new SelectItem { id = item.Id.ToString(), text = item.Name });
                pList.Add(hosp);
            }
            return pList;
        }

        public async Task<PagedData<DoctorListView>> GetDoctorList(QueryDoctorRequest request)
        {
            var query = _repository.GetDbQuerySet();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(d => d.Name.Contains(request.SearchKey) || d.User.UserName.Contains(request.SearchKey));
            }
            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_DoctorListView());
            return await Task.FromResult(pageData);

        }

        public async Task<AddDoctorResponse> AddDoctor(DoctorListView request)
        {
            var res = new AddDoctorResponse();
            string newUserId = null;
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                UserCreateRequest userCreate = new UserCreateRequest();
                userCreate.Name = request.Name;
                userCreate.RoleName = RoleType.doctor.ToString();
                userCreate.UserName = request.UserName;
                userCreate.HospitalId = request.HospitalId;
                newUserId = await _userService.CreateUserReturnUserId(userCreate);
                if (newUserId == null)
                {
                    throw new Exception("创建医生登录用户失败");
                }
            }

            var info = Mapper.Map<TBDoctor>(request);
            info.AddUserName = GetLoginUserName();
            if (newUserId != null)
            {
                info.UserId = newUserId;
            }
            //  var hosp = await _hospitalRepository.FindSingleAsync(d => d.Id == info.HospitalId);
            //   info.Hospital = hosp;
            info.Hospital = null;
            await _repository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Doctor);
                res.Result = true;
                res.SuccessMessage = "新增成功";
                res.Id = info.Id;
            }
            else
            {
                res.Result = false;
                res.ErrorMessage = "新增失败";
            }
            return res;
        }

        public async Task<EmptyResponse> SaveDoctorImage(int Id, string url)
        {
            var info = await _repository.GetAsync(Id);
            info.ImageUrl = url;
            await _repository.UpdateAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("图片保存成功") : EmptyResponse.CreateError("图片保存失败");
        }


        public async Task<EditDoctorResponse> EditDoctor(DoctorListView request)
        {
            var res = new EditDoctorResponse();
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                await _userService.UpdateUserNameAsync(request.UserId, request.UserName);
            }

            var info = await _repository.GetAsync(request.Id);
            info = Mapper.Map(request, info);
            info.ModifyUserName = GetLoginUserName();

            //var hosp = await _hospitalRepository.FindSingleAsync(d => d.Id == info.HospitalId);
            // info.Hospital = hosp;
            await _repository.UpdateAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Doctor);
                res.Result = true;
                res.SuccessMessage = "编辑成功";
                res.Id = info.Id;
            }
            else
            {
                res.Result = false;
                res.ErrorMessage = "编辑失败";
            }
            return res;
        }

        public async Task<EmptyResponse> DeleteDoctor(OnlyHasIdRequest request)
        {
            var info = await _repository.GetAsync(request.Id);
            var userId = info.UserId;
            await _repository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await _userService.DeleteUserAsync(userId);
                }
                _cacheService.RemoveCache(CacheKeys.Doctor);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }

    }
}
