using AutoMapper;
using BYBY.Cache;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BYBY.Services.Models;

namespace BYBY.Services.Implementations
{
    public class HospitalService : BaseService, IHospitalService
    {

        readonly IRepository<TBConsultationRoom, int> _roomRepository;
        readonly IRepository<TBDateSetup, int> _dateRepository;
        readonly IRepository<TBPlan, int> _planRepository;
        readonly IUnitOfWork _unitOfWork;
        readonly ICacheService _cacheService;

        public HospitalService(IRepository<TBConsultationRoom, int> roomRepository,
            IRepository<TBDateSetup, int> dateRepository,
            IRepository<TBPlan, int> planRepository,
            ICacheService cacheService,
            IUnitOfWork unitOfWork
            )
        {
            _roomRepository = roomRepository;
            _dateRepository = dateRepository;
            _planRepository = planRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }




        /// <summary>
        /// 获取会诊室列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ConsultationRoomListView>> GetRoomList()
        {
            IList<TBConsultationRoom> list;
            var hosptitalId = LoginUserInfo.HospitalId;
            //if (hosptitalId == 0)
            //{
            //    list = _roomRepository.GetDbQuerySet().OrderBy(d => d.HospitalId).ToList();
            //}
            //else
            //{
            list = (await _roomRepository.FindAsync(d => d.HospitalId == hosptitalId)).OrderBy(d => d.HospitalId).ToList();
            //  }

            var views = Mapper.Map<IList<ConsultationRoomListView>>(list);
            return views;
        }


        /// <summary>
        /// 新增会诊室
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddRoom(AddRoomRequest request)
        {
            var hospitalId = LoginUserHospitalId;
            bool exists = await CheckRoomNameExists(request.Name, hospitalId);
            if (exists)
            {
                return EmptyResponse.CreateError("诊室名称不能重复");
            }
            var info = new TBConsultationRoom { Name = request.Name, HospitalId = hospitalId };
            info.AddUserName = GetLoginUserName();
            await _roomRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Room);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        private async Task<bool> CheckRoomNameExists(string name, int hospitalId, int id = 0)
        {
            if (id == 0)
            {
                var icout = await _roomRepository.FindCountAsync(d => d.Name == name && d.HospitalId == hospitalId);
                return icout > 0;
            }
            else
            {
                var icout = await _roomRepository.FindCountAsync(d => d.Name == name && d.HospitalId == hospitalId && d.Id != id);
                return icout > 0;
            }
        }

        /// <summary>
        /// 编辑会诊室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> EditRoom(ConsultationRoomListView request)
        {
            bool exists = await CheckRoomNameExists(request.Name, request.HospitalId, request.Id);
            if (exists)
            {
                return EmptyResponse.CreateError("诊室名称不能重复");
            }
            var editInfo = await _roomRepository.GetAsync(request.Id);
            editInfo.Name = request.Name;

            editInfo.ModifyUserName = GetLoginUserName();
            await _roomRepository.UpdateAsync(editInfo);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Room);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("编辑成功") : EmptyResponse.CreateError("编辑失败");
        }


        /// <summary>
        ///  删除会诊室
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteRoom(OnlyHasIdRequest request)
        {
            var info = await _roomRepository.GetAsync(request.Id);
            await _roomRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Room);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }



        public async Task<IList<DateSetupListView>> GetDateSetupList()
        {
            var query = await _dateRepository.FindAllAsync();
            var list = Mapper.Map<IList<DateSetupListView>>(query);
            return list;
        }

        public async Task<EmptyResponse> AddDateSetup(AddDateSetupRequest request)
        {

            DateTime stime = DateTime.Parse("2000-01-01 " + request.STime);
            DateTime etime = DateTime.Parse("2000-01-01 " + request.ETime);
            var isDup = await CheckDateSetupDup(stime, etime);
            if (isDup)
            {
                return EmptyResponse.CreateError("会诊时段不能重复");
            }
            var info = new TBDateSetup { STime = stime, ETime = etime, DefaultPeople = request.DefaultPeople };
            info.AddUserName = GetLoginUserName();
            await _dateRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        private async Task<bool> CheckDateSetupDup(DateTime stime, DateTime etime)
        {
            return await _dateRepository.ExistAsync(d => (d.ETime > stime && d.ETime <= etime)
             || (d.STime >= stime && d.STime < etime) || (d.STime <= stime && d.ETime >= etime));
        }

        public async Task<EmptyResponse> DeleteDateSetup(OnlyHasIdRequest request)
        {
            var info = await _dateRepository.GetAsync(request.Id);
            await _dateRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }


        public async Task<PlanListView> GetPlanList(PlanQueryRequest request)
        {
            var query = await _dateRepository.FindAllAsync();
            var hospitalId = LoginUserHospitalId;
            PlanListView plan = new PlanListView();
            DateSetupView dataView;
            DateTime monday = DateTime.MinValue;
            DateTime dateSelect = string.IsNullOrWhiteSpace(request.DateSelect) ? DateTime.Now : DateTime.Parse(request.DateSelect);

            switch (request.WeekSelect)
            {
                case WeekSelect.PrevWeek:
                    monday = dateSelect.AddDays(-7).StartOfWeek(DayOfWeek.Monday);
                    break;
                case WeekSelect.NextWeek:
                    monday = dateSelect.AddDays(7).StartOfWeek(DayOfWeek.Monday);
                    break;
                case WeekSelect.ThisWeek:
                    monday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    break;
                default:
                    monday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    break;
            }
            var nextMonday = monday.AddDays(7);
            var planDatas = await _planRepository.FindAsync(d => d.PlanDate >= monday && d.PlanDate < nextMonday && d.Doctor.HospitalId == hospitalId);
            if (request.RoomId > 0)
            {
                planDatas = planDatas.Where(d => d.RoomId == request.RoomId);
            }
            if (request.DoctorId > 0)
            {
                planDatas = planDatas.Where(d => d.DoctorId == request.DoctorId);
            }
            var planDataList = planDatas.ToList();
            DateTime stepDate = DateTime.MinValue;
            //  DateTime stepDateNext = DateTime.MinValue;
            PlanView planView;
            TBPlan tablePlan;
            DateTime _stime = DateTime.MinValue;
            DateTime _etime = DateTime.MinValue;
            foreach (var dataSetup in query)
            {
                dataView = new DateSetupView();
                dataView.STime = dataSetup.STime.ToString("HH:mm");
                dataView.ETime = dataSetup.ETime.ToString("HH:mm");

                for (int i = 0; i < 7; i++)
                {

                    stepDate = monday.AddDays(i);
                    _stime = DateTime.Parse(stepDate.ToString("yyyy-MM-dd ") + dataView.STime);
                    _etime = DateTime.Parse(stepDate.ToString("yyyy-MM-dd ") + dataView.ETime);
                    tablePlan = planDataList.FirstOrDefault(d => d.STime == _stime && d.ETime == _etime);
                    if (tablePlan != null)
                    {
                        planView = Mapper.Map<PlanView>(tablePlan);
                        planView.ConsultationCount = tablePlan.ValidConsultations.Count();
                        planView.ConsultationList = Mapper.Map<IList<PlanConsultationView>>(tablePlan.ValidConsultations);
                    }
                    else
                    {
                        planView = new PlanView();
                        planView.STime = _stime.ToString("yyyy-MM-dd HH:mm:ss");
                        planView.ETime = _etime.ToString("yyyy-MM-dd HH:mm:ss");
                        planView.People = dataSetup.DefaultPeople;
                        planView.PlanDate = stepDate.ToString("yyyy-MM-dd HH:mm:ss");
                        planView.RoomId = request.RoomId;
                        //if (request.DoctorId > 0)
                        //{
                        //    planView.DoctorId = request.DoctorId;
                        //}

                    }
                    dataView.OneDayPlans.Add(planView);
                }
                plan.DateViews.Add(dataView);
            }
            string weekTitle = "";
            for (int i = 0; i < 7; i++)
            {
                stepDate = monday.AddDays(i);
                weekTitle = stepDate.ToString("MM-dd") + " (" + stepDate.DayOfWeek.GetDayOfWeekText() + ")";
                plan.WeekTitles.Add(weekTitle);
            }
            plan.DateSelect = monday.ToString("yyyy-MM-dd");
            plan.EndDate = monday.AddDays(6).ToString("yyyy-MM-dd");
            return plan;
        }

        public async Task<EmptyResponse> SavePlan(IList<DateSetupView> request)
        {
            TBPlan info;
            var user = GetLoginUserName();
            foreach (var dataView in request)
            {
                foreach (var planView in dataView.OneDayPlans)
                {

                    if (planView.Id > 0)
                    {

                        info = await _planRepository.GetAsync(planView.Id);
                        info = Mapper.Map(planView, info);
                        if (planView.DoctorId > 0)
                        {
                            await CheckDoctorIsAlreadyPlan(info);
                            info.ModifyUserName = user;
                            await _planRepository.UpdateAsync(info);
                        }
                        else
                        {
                          
                            await _planRepository.DeleteAsync(info);
                        }

                    }
                    else if (planView.DoctorId > 0)
                    {
                        info = Mapper.Map<TBPlan>(planView);
                        await CheckDoctorIsAlreadyPlan(info);
                        info.AddUserName = user;
                        await _planRepository.InsertAsync(info);
                    }
                }
            }
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        private async Task CheckDoctorIsAlreadyPlan(TBPlan plan)
        {
            TBPlan hasPlan = null;
            if (plan.Id > 0)
            {
                //检查当前时段此会诊室是否已有排班
                hasPlan = await _planRepository.FindSingleAsync(d => d.RoomId == plan.RoomId && d.STime == plan.STime && d.ETime == plan.ETime && d.Id != plan.Id);
                if (hasPlan != null)
                {
                    throw new Exception(string.Format("会诊室：{0}在 {1} 到 {2} 时段已有医生：{3}排班，请重新设置排班",
                        hasPlan.Room.Name, hasPlan.STime.ToDateTimeString(), hasPlan.ETime.ToDateTimeString(), hasPlan.Doctor.Name));
                }

                //检查此医生在当前时间段是否已有排班
                hasPlan = await _planRepository.FindSingleAsync(d => d.DoctorId == plan.DoctorId && d.STime == plan.STime && d.ETime == plan.ETime && d.Id != plan.Id);
                if (hasPlan != null)
                {
                    throw new Exception(string.Format("医生：{0}在 {1} 到 {2} 时段已有排班，请重新设置排班",
                        hasPlan.Doctor.Name, hasPlan.STime.ToDateTimeString(), hasPlan.ETime.ToDateTimeString()));
                }
            }
            else
            {
                //检查当前时段此会诊室是否已有排班
                hasPlan = await _planRepository.FindSingleAsync(d => d.RoomId == plan.RoomId && d.STime == plan.STime && d.ETime == plan.ETime);
                if (hasPlan != null)
                {
                    throw new Exception(string.Format("会诊室：{0}在 {1} 到 {2} 时段已有医生：{3}排班，请重新设置排班",
                        hasPlan.Room.Name, hasPlan.STime.ToDateTimeString(), hasPlan.ETime.ToDateTimeString(), hasPlan.Doctor.Name));
                }
                //检查此医生在当前时间段是否已有排班
                hasPlan = await _planRepository.FindSingleAsync(d => d.DoctorId == plan.DoctorId && d.STime == plan.STime && d.ETime == plan.ETime);
                if (hasPlan != null)
                {
                    throw new Exception(string.Format("医生：{0}在 {1} 到 {2} 时段已有排班，请重新设置排班",
                        hasPlan.Doctor.Name, hasPlan.STime.ToDateTimeString(), hasPlan.ETime.ToDateTimeString()));
                }

            }

        }




    }
}
