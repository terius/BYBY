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
            var query = await _roomRepository.FindAllAsync();
            var list = Mapper.Map<IList<ConsultationRoomListView>>(query);
            return list;
        }


        /// <summary>
        /// 新增会诊室
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddRoom(AddRoomRequest request)
        {
            bool exists = await CheckRoomNameExists(request.Name);
            if (exists)
            {
                return EmptyResponse.CreateError("诊室名称不能重复");
            }
            var info = new TBConsultationRoom { Name = request.Name };
            info.AddUserName = GetLoginUserName();
            await _roomRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                _cacheService.RemoveCache(CacheKeys.Room);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        private async Task<bool> CheckRoomNameExists(string name, int id = 0)
        {
            if (id == 0)
            {
                var icout = await _roomRepository.FindCount(d => d.Name == name);
                return icout > 0;
            }
            else
            {
                var icout = await _roomRepository.FindCount(d => d.Name == name && d.Id != id);
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
            bool exists = await CheckRoomNameExists(request.Name, request.Id);
            if (exists)
            {
                return EmptyResponse.CreateError("诊室名称不能重复");
            }
            var editInfo = await _roomRepository.GetAsync(request.Id);
            editInfo = Mapper.Map(request, editInfo);
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
            var info = new TBDateSetup { STime = stime, ETime = etime, DefaultPeople = request.DefaultPeople };
            info.AddUserName = GetLoginUserName();
            await _dateRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
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
            var planDatas = (await _planRepository.FindAsync(d => d.PlanDate >= monday && d.PlanDate < nextMonday)).ToList();
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
                    tablePlan = planDatas.FirstOrDefault(d => d.STime == _stime && d.ETime == _etime);
                    if (tablePlan != null)
                    {
                        planView = Mapper.Map<PlanView>(tablePlan);
                    }
                    else
                    {
                        planView = new PlanView();
                        planView.STime = _stime;
                        planView.ETime = _etime;
                        planView.People = dataSetup.DefaultPeople;
                        planView.PlanDate = stepDate;

                    }
                    dataView.OneDayPlans.Add(planView);
                }
                plan.DateViews.Add(dataView);
            }
            string weekTitle = "";
            for (int i = 0; i < 7; i++)
            {
                stepDate = monday.AddDays(i);
                weekTitle = stepDate.ToString("MM-dd") + "(" + stepDate.DayOfWeek.GetDayOfWeekText() + ")";
                plan.WeekTitles.Add(weekTitle);
            }
            plan.DateSelect = monday.ToString("yyyy-MM-dd");
            return plan;
        }

    }
}
