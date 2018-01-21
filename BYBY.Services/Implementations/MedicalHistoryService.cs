using AutoMapper;
using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.Helpers;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using BYBY.Services.Models;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BYBY.Services.Implementations
{
    public class MedicalHistoryService : BaseService, IMedicalHistoryService
    {

        readonly IRepository<TBMedicalHistory, int> _repository;
        readonly IRepository<TBMedicalDetail, int> _medicalDetailRepository;
        readonly IRepository<TBConsultation, int> _consultationRepository;
        readonly IRepository<TBReferral, int> _referralRepository;
        readonly IRepository<TBPatient, int> _patientRepository;
        readonly IRepository<TBMedicalHistoryImage, int> _imageRepository;
        readonly IRepository<TBConsultationMedicine, int> _consultationMedicineRepository;
        readonly IRepository<TBMedicine, int> _medicineRepository;
        readonly IRepository<TBConsultationCheck, int> _consultationCheckRepository;
        readonly IRepository<TBCheckAssay, int> _checkRepository;
        readonly IRepository<TBHospital, int> _hospitalRepository;
        readonly IUnitOfWork _unitOfWork;

        public MedicalHistoryService(IRepository<TBMedicalHistory, int> repository,
            IRepository<TBMedicalDetail, int> medicalDetailRepository,
            IRepository<TBConsultation, int> consultationRepository,
            IRepository<TBReferral, int> referralRepository,
            IRepository<TBPatient, int> patientRepository,
            IRepository<TBMedicalHistoryImage, int> imageRepository,
            IRepository<TBConsultationMedicine, int> consultationMedicineRepository,
            IRepository<TBMedicine, int> medicineRepository,
            IRepository<TBConsultationCheck, int> consultationCheckRepository,
           IRepository<TBCheckAssay, int> checkRepository,
           IRepository<TBHospital, int> hospitalRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _medicalDetailRepository = medicalDetailRepository;
            _consultationRepository = consultationRepository;
            _referralRepository = referralRepository;
            _patientRepository = patientRepository;
            _imageRepository = imageRepository;
            _consultationMedicineRepository = consultationMedicineRepository;
            _medicineRepository = medicineRepository;
            _consultationCheckRepository = consultationCheckRepository;
            _checkRepository = checkRepository;
            _hospitalRepository = hospitalRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<PagedData<MedicalHistoryListView>> GetMedicalHistoryList(MedicalHistoryListSearchRequest request)
        {
            var query = _repository.GetDbQuerySet();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(d => d.MalePatient.Name.StartsWith(request.SearchKey)
                || d.MalePatient.CardNo.StartsWith(request.SearchKey)
                || d.FeMalePatient.Name.StartsWith(request.SearchKey)
                || d.FeMalePatient.CardNo.StartsWith(request.SearchKey)
                );
            }

            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_MedicalHistoryListViews());
            return await Task.FromResult(pageData);

        }

        public async Task<EmptyResponse> SaveAdd(MedicalHistoryAddRequest request)
        {
            var femaleInfo = request.C_To_FemaleTBPatient();
            var maleInfo = request.C_To_MaleTBPatient();
            var mhInfo = new TBMedicalHistory();
            mhInfo.Address = request.Address;
            mhInfo.AddUserName = femaleInfo.AddUserName = maleInfo.AddUserName = GetLoginUserName();
            mhInfo.FeMalePatient = femaleInfo;
            mhInfo.MalePatient = maleInfo;
            mhInfo.LandlinePhone = request.FixPhone;
            mhInfo.MedicalHistoryNo = request.MedicalHistoryNo;
            mhInfo.Remark = request.Remark;
            await _repository.InsertAsync(mhInfo);
            int rs = _unitOfWork.Commit();

            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        public async Task<EmptyResponse> SaveEditBaseInfo(MedicalHistoryEditRequest request)
        {
            var mhInfo = await _repository.GetAsync(request.MHId);
            if (mhInfo == null)
            {
                throw new NullReferenceException("病历信息为空，无法修改");
            }
            var femaleInfo = request.C_To_FemaleTBPatient(mhInfo.FeMalePatient);
            var maleInfo = request.C_To_MaleTBPatient(mhInfo.MalePatient);

            mhInfo.Address = request.Address;
            mhInfo.AddUserName = femaleInfo.AddUserName = maleInfo.AddUserName = GetLoginUserName();
            mhInfo.FeMalePatient = femaleInfo;
            mhInfo.MalePatient = maleInfo;
            mhInfo.LandlinePhone = request.FixPhone;
            mhInfo.MedicalHistoryNo = request.MedicalHistoryNo;
            mhInfo.Remark = request.Remark;
            await _repository.UpdateAsync(mhInfo);
            int rs = _unitOfWork.Commit();

            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        public async Task<MedicalHistoryDetailModel> GetDetailModel(int id)
        {
            var info = await _repository.GetAsync(id);
            var model = new MedicalHistoryDetailModel();
            model.EditModel = info.C_To_EditView();
            model.FemaleMedicalDetails = await GetMedicalDetails(info.FeMalePatient);
            model.MaleMedicalDetails = await GetMedicalDetails(info.MalePatient);
            model.Images = Mapper.Map<List<MedicalHistoryImageRequest>>(info.TBMedicalHistoryImages.OrderBy(d => d.Id));
            return model;
        }


        public async Task<IList<MedicalDetailRequest>> GetMedicalDetails(TBPatient patient)
        {
            var rs = Task.Run(() =>
           {
               var view = patient.MedicalDetails.OrderByDescending(d => d.AddTime).C_To_MedicalDetailRequests();

               return view;
           }).Result;


            return await Task.FromResult(rs);
        }


        public async Task<EmptyResponse> SaveEditMedicalDetail(MedicalDetailRequest request)
        {
            var mhInfo = await _medicalDetailRepository.GetAsync(request.MDId);
            if (mhInfo == null)
            {
                throw new NullReferenceException("病历详细信息为空，无法修改");
            }
            mhInfo = Mapper.Map(request, mhInfo);
            await _medicalDetailRepository.UpdateAsync(mhInfo);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }


        public async Task<EmptyResponse> SaveAddMedicalDetail(MedicalDetailAddRequest request)
        {
            TBMedicalDetail info = Mapper.Map<TBMedicalDetail>(request);
            info.Title = await GetMedicalDetailTitle(request.PatientId);
            await _medicalDetailRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        private async Task<string> GetMedicalDetailTitle(int patientId)
        {
            var icount = await _medicalDetailRepository.FindCountAsync(d => d.PatientId == patientId);
            return icount <= 0 ? "初诊病历" : ("复诊病历" + icount);
        }





        public async Task<EmptyResponse> Delete(MedicalHistoryDeleteRequest request)
        {
            var info = await _repository.GetAsync(request.Id);
            int femaleId = info.FeMalePatientId;
            int maleId = info.MalePatientId;
            await _repository.DeleteAsync(info);
            //删除女方患者
            var female = await _patientRepository.GetAsync(femaleId);
            await _patientRepository.DeleteAsync(female);

            //删除男方患者
            var male = await _patientRepository.GetAsync(maleId);
            await _patientRepository.DeleteAsync(male);

            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }

        /// <summary>
        /// 保存病历图片
        /// </summary>
        /// <param name="filePaths"></param>
        /// <param name="MHId"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> SaveMHImage(IList<string> filePaths, int MHId)
        {
            TBMedicalHistoryImage imageInfo;
            var loginUserName = GetLoginUserName();
            foreach (var filepath in filePaths)
            {
                imageInfo = new TBMedicalHistoryImage();
                imageInfo.FilePath = filepath;
                imageInfo.Name = filepath.Substring(filepath.LastIndexOf('/') + 1);
                imageInfo.TBMedicalHistoryId = MHId;
                imageInfo.AddUserName = loginUserName;
                await _imageRepository.InsertAsync(imageInfo);
            }
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("上传成功") : EmptyResponse.CreateError("上传失败");
        }

        /// <summary>
        /// 删除病历附件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteImage(MedicalHistoryImageDeleteRequest request)
        {
            var info = await _imageRepository.GetAsync(request.Id);
            string filePath = info.FilePath;
            await _imageRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {

                filePath = HttpContext.Current.Server.MapPath(filePath);
                FileHelper.DeleteFile(filePath);
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }


        #region 会诊模块
        public async Task<EmptyResponse> SaveConsultationAdd(ConsultationAddRequest request)
        {
            var loginUserName = GetLoginUserName();
            var info = Mapper.Map<TBConsultation>(request);
            info.AddUserName = loginUserName;
            info.ConsultationStatus = ConsultationStatus.Requesting;
            await _consultationRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                //更新病历会诊状态
                var mdInfo = await _repository.GetAsync(request.TBMedicalHistoryId);
                mdInfo.ConsultationStatus = ConsultationStatus.Requesting;
                mdInfo.ModifyUserName = loginUserName;
                mdInfo.NewestConsultationId = info.Id;
                await _repository.UpdateAsync(mdInfo);
                rs = _unitOfWork.Commit();
                if (rs <= 0)
                {
                    await _consultationRepository.DeleteAsync(info);
                    _unitOfWork.Commit();
                }
            }

            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        public async Task<EmptyResponse> UpdateConsultationToCancel(ConsultationCancelRequest cancelRequest)
        {
            var loginUserName = GetLoginUserName();
            var MHInfo = await _repository.GetAsync(cancelRequest.MHId);
            MHInfo.ConsultationStatus = ConsultationStatus.Cancel;
            MHInfo.ModifyUserName = loginUserName;
            await _repository.UpdateAsync(MHInfo);

            var cInfo = await _consultationRepository.GetAsync(MHInfo.NewestConsultationId.Value);
            cInfo.ConsultationStatus = ConsultationStatus.Cancel;
            cInfo.ModifyUserName = loginUserName;
            await _consultationRepository.UpdateAsync(cInfo);

            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("会诊取消成功") : EmptyResponse.CreateError("会诊取消失败");
        }

        public async Task<PagedData<ConsultationListView>> GetConsultationList(ConsultationQueryRequest request)
        {
            var query = _consultationRepository.GetDbQuerySet();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(d => d.MedicalHistory.MalePatient.Name.StartsWith(request.SearchKey)
                || d.MedicalHistory.MalePatient.CardNo.StartsWith(request.SearchKey)
                || d.MedicalHistory.FeMalePatient.Name.StartsWith(request.SearchKey)
                || d.MedicalHistory.FeMalePatient.CardNo.StartsWith(request.SearchKey)
                );
            }
            if (!request.IsAll)
            {
                //  Expression<Func<TBConsultation, bool>> predicate = d => d.ConsultationStatus == ConsultationStatus.Cancel;
                var predicate = PredicateBuilder.False<TBConsultation>();
                if (request.IsCancel)
                {
                    predicate = predicate.Or(d => d.ConsultationStatus == ConsultationStatus.Cancel);
                }

                if (request.IsRequest)
                {
                    predicate = predicate.Or(d => d.ConsultationStatus == ConsultationStatus.Requesting);
                }

                if (request.IsConfirm)
                {
                    predicate = predicate.Or(d => d.ConsultationStatus == ConsultationStatus.Confirm);
                }

                if (request.IsComplete)
                {
                    predicate = predicate.Or(d => d.ConsultationStatus == ConsultationStatus.Complete);
                }

                query = query.Where(predicate);
            }

            DateTime sdt = DateTime.MinValue;
            if (DateTime.TryParse(request.Stime, out sdt))
            {
                query = query.Where(d => d.RequestDate >= sdt);
            }


            DateTime edt = DateTime.MinValue;
            if (DateTime.TryParse(request.Etime, out edt))
            {
                query = query.Where(d => d.RequestDate <= edt);
            }

            if (request.HospitalId.HasValue)
            {
                query = query.Where(d => d.HospitalId == request.HospitalId.Value);
            }

            query = query.Where(d => d.Id == d.MedicalHistory.NewestConsultationId);
            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ConsultationListViews());
            return await Task.FromResult(pageData);

        }


        public async Task<ConsultationDetailModel> GetConsultationDetail(int id)
        {
            var info = await _consultationRepository.GetAsync(id);
            //   var info =  _consultationRepository.GetDbQuerySet().First(d => d.Id == id);
            var model = info.C_To_ConsultationDetailModel();
            return model;
        }


        public async Task<EmptyResponse> SaveConsultationRecord(ConsultationRecordEditRequest request)
        {
            var editInfo = await _consultationRepository.GetAsync(request.MyId);
            editInfo = Mapper.Map(request, editInfo);
            editInfo.ModifyUserName = editInfo.RecordUser = GetLoginUserName();
            editInfo.RecordTime = DateTime.Now;
            await _consultationRepository.UpdateAsync(editInfo);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        public async Task<EmptyResponse> UpdateConsultationStatus(UpdateConsultationStatusRequest request)
        {
            var loginUserName = GetLoginUserName();
            var cInfo = await _consultationRepository.GetAsync(request.Id);
            cInfo.ConsultationStatus = cInfo.MedicalHistory.ConsultationStatus = request.Status;
            cInfo.ModifyUserName = cInfo.MedicalHistory.ModifyUserName = loginUserName;
            await _consultationRepository.UpdateAsync(cInfo);
            int rs = _unitOfWork.Commit();
            string msg = "";
            switch (request.Status)
            {
                case ConsultationStatus.No:
                    break;
                case ConsultationStatus.Requesting:
                    msg = "申请";
                    break;
                case ConsultationStatus.Cancel:
                    msg = "取消";
                    break;
                case ConsultationStatus.Confirm:
                    msg = "确认";
                    break;
                case ConsultationStatus.Complete:
                    msg = "完成";
                    break;
                default:
                    break;
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("会诊已" + msg) : EmptyResponse.CreateError("会诊" + msg + "失败");
        }


        public async Task<MainModel> GetMainModel()
        {
            MainModel model = new MainModel();
            var status = (await _consultationRepository.FindAsync(d => d.Id == d.MedicalHistory.NewestConsultationId)).Select(d => d.ConsultationStatus).ToList();
            model.ConsultationCancelCount = status.Count(d => d == ConsultationStatus.Cancel);
            model.ConsultationConfirmCount = status.Count(d => d == ConsultationStatus.Confirm);
            model.ConsultationRequestCount = status.Count(d => d == ConsultationStatus.Requesting);

            var rstatus = (await _referralRepository.FindAsync(d => d.Id == d.MedicalHistory.NewestReferralId)).Select(d => d.ReferralStatus).ToList();
            model.ReferralCancelCount = rstatus.Count(d => d == ReferralStatus.Cancel);
            model.ReferralConfirmCount = rstatus.Count(d => d == ReferralStatus.Confirm);
            model.ReferralRequestCount = rstatus.Count(d => d == ReferralStatus.Requesting);



            return model;

        }


        #region 会诊详细信息-药品

        /// <summary>
        /// 新增药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddMedicine(ConsultationMedicineListRequest request)
        {
            var info = Mapper.Map<TBConsultationMedicine>(request);
            info.AddUserName = GetLoginUserName();
            info.Medicine = await _medicineRepository.GetAsync(request.MedicineId);
            await _consultationMedicineRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        /// <summary>
        /// 修改药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> EditMedicine(ConsultationMedicineListRequest request)
        {
            var info = await _consultationMedicineRepository.GetAsync(request.Id);
            info = Mapper.Map(request, info);
            info.ModifyUserName = GetLoginUserName();
            await _consultationMedicineRepository.UpdateAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("编辑成功") : EmptyResponse.CreateError("编辑失败");
        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteMedicine(OnlyHasIdRequest request)
        {
            var info = await _consultationMedicineRepository.GetAsync(request.Id);
            await _consultationMedicineRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }
        #endregion


        #region 会诊详细信息-检查项目

        /// <summary>
        /// 新增检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> AddCheck(ConsultationCheckListRequest request)
        {
            var info = Mapper.Map<TBConsultationCheck>(request);
            info.AddUserName = GetLoginUserName();
            info.CheckAssay = await _checkRepository.GetAsync(request.CheckId);
            await _consultationCheckRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("新增成功") : EmptyResponse.CreateError("新增失败");
        }

        /// <summary>
        /// 修改检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> EditCheck(ConsultationCheckListRequest request)
        {
            var info = await _consultationCheckRepository.GetAsync(request.Id);
            info = Mapper.Map(request, info);
            info.ModifyUserName = GetLoginUserName();
            await _consultationCheckRepository.UpdateAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("编辑成功") : EmptyResponse.CreateError("编辑失败");
        }

        /// <summary>
        /// 删除检查项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<EmptyResponse> DeleteCheck(OnlyHasIdRequest request)
        {
            var info = await _consultationCheckRepository.GetAsync(request.Id);
            await _consultationCheckRepository.DeleteAsync(info);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("删除成功") : EmptyResponse.CreateError("删除失败");
        }
        #endregion


        #endregion


        #region 转诊模块
        public async Task<EmptyResponse> SaveReferralAdd(ReferralAddRequest request)
        {
            var loginUserName = GetLoginUserName();
            var info = Mapper.Map<TBReferral>(request);
            info.AddUserName = loginUserName;
            info.ReferralStatus = ReferralStatus.Requesting;
            await _referralRepository.InsertAsync(info);
            int rs = _unitOfWork.Commit();
            if (rs > 0)
            {
                //更新病历会诊状态
                var mdInfo = await _repository.GetAsync(request.TBMedicalHistoryId);
                mdInfo.ReferralStatus = ReferralStatus.Requesting;
                mdInfo.NewestReferralId = info.Id;
                mdInfo.ModifyUserName = loginUserName;
                await _repository.UpdateAsync(mdInfo);
                rs = _unitOfWork.Commit();
                if (rs <= 0)
                {
                    await _referralRepository.DeleteAsync(info);
                    _unitOfWork.Commit();
                }
            }


            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }




        public async Task<EmptyResponse> UpdateReferralToCancel(ReferralCancelRequest cancelRequest)
        {
            var loginUserName = GetLoginUserName();
            var MHInfo = await _repository.GetAsync(cancelRequest.MHId);
            MHInfo.ReferralStatus = ReferralStatus.Cancel;
            MHInfo.ModifyUserName = loginUserName;
            await _repository.UpdateAsync(MHInfo);

            var cInfo = await _referralRepository.GetAsync(MHInfo.Referrals.OrderByDescending(d => d.Id).First().Id);
            cInfo.ReferralStatus = ReferralStatus.Cancel;
            cInfo.ModifyUserName = loginUserName;
            await _referralRepository.UpdateAsync(cInfo);

            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("转诊取消成功") : EmptyResponse.CreateError("转诊取消失败");
        }

        public async Task<PagedData<ReferralListView>> GetReferralList(ReferralQueryRequest request)
        {
            var query = _referralRepository.GetDbQuerySet();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(d => d.MedicalHistory.MalePatient.Name.StartsWith(request.SearchKey)
                || d.MedicalHistory.MalePatient.CardNo.StartsWith(request.SearchKey)
                || d.MedicalHistory.FeMalePatient.Name.StartsWith(request.SearchKey)
                || d.MedicalHistory.FeMalePatient.CardNo.StartsWith(request.SearchKey)
                );
            }
            if (!request.IsAll)
            {
                //  Expression<Func<TBConsultation, bool>> predicate = d => d.ConsultationStatus == ConsultationStatus.Cancel;
                var predicate = PredicateBuilder.False<TBReferral>();
                if (request.IsCancel)
                {
                    predicate = predicate.Or(d => d.ReferralStatus == ReferralStatus.Cancel);
                }

                if (request.IsRequest)
                {
                    predicate = predicate.Or(d => d.ReferralStatus == ReferralStatus.Requesting);
                }

                if (request.IsConfirm)
                {
                    predicate = predicate.Or(d => d.ReferralStatus == ReferralStatus.Confirm);
                }

                query = query.Where(predicate);
            }
            DateTime sdt = DateTime.MinValue;
            if (DateTime.TryParse(request.Stime, out sdt))
            {
                query = query.Where(d => d.RequestDate >= sdt);
            }


            DateTime edt = DateTime.MinValue;
            if (DateTime.TryParse(request.Etime, out edt))
            {
                query = query.Where(d => d.RequestDate <= edt);
            }

            query = query.Where(d => d.Id == d.MedicalHistory.NewestReferralId);
            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ReferralListViews());
            return await Task.FromResult(pageData);

        }


        public async Task<EmptyResponse> UpdateReferralStatus(UpdateReferralStatusRequest request)
        {
            var loginUserName = GetLoginUserName();
            var info = await _referralRepository.GetAsync(request.Id);
            info.ReferralStatus = info.MedicalHistory.ReferralStatus = request.Status;
            info.ModifyUserName = info.MedicalHistory.ModifyUserName = loginUserName;
            await _referralRepository.UpdateAsync(info);
            int rs = _unitOfWork.Commit();
            string msg = "";
            switch (request.Status)
            {
                case ReferralStatus.No:
                    break;
                case ReferralStatus.Requesting:
                    msg = "申请";
                    break;
                case ReferralStatus.Cancel:
                    msg = "取消";
                    break;
                case ReferralStatus.Confirm:
                    msg = "确认";
                    break;
                default:
                    break;
            }
            return rs > 0 ? EmptyResponse.CreateSuccess("转诊已" + msg) : EmptyResponse.CreateError("转诊" + msg + "失败");
        }


        public async Task<IList<DisplayModel>> GetReferralDetail(OnlyHasIdRequest request)
        {
            var info = await _referralRepository.GetAsync(request.Id);
            var view = info.C_To_ReferralListView();
            var detailView = GetDisplayView(view);
            return detailView;
        }

        #endregion


        #region 患者报表
        public async Task<ReportView> GetReport(ReportQueryRequest request)
        {
            var view = new ReportView();
            DateTime stime = Convert.ToDateTime(request.STime);
            DateTime etime = Convert.ToDateTime(request.ETime);
            int totalDays = (int)etime.Subtract(stime).TotalDays;
            view.XDates = new string[totalDays];
            ReportItemView reportItem;
            DateTime day = stime.Date;
            //    int masterHospital = LoginUserHospitalId
            //  var hosps = (await _hospitalRepository.FindAsync(d => d.MasterHospitals.Any(f => f.Id == masterHospital))).ToList();
            IList<TBHospital> hosps = null;
            if (IsMasterDoctor)
            {
                hosps = await GetLoginUserChildHospList();
                if (request.HospitalId > 0)
                {
                    hosps = hosps.Where(d => d.Id == request.HospitalId).ToList();
                }
            }
            else
            {

                hosps = (await _hospitalRepository.FindAsync(d => d.Id == LoginUserHospitalId)).ToList();
            }

            view.Hospitals = hosps.Select(d => d.Name).ToArray();
            if (request.Type == 1)
            {
                IList<TBConsultation> oneDayData;
                var query = await _consultationRepository.FindAsync(d => d.ConsultationStatus == ConsultationStatus.Complete && d.Plan.STime >= stime && d.Plan.ETime <= etime && d.Id == d.MedicalHistory.NewestConsultationId);


                foreach (var hosp in hosps)
                {
                    reportItem = new ReportItemView { name = hosp.Name, type = "bar", data = new int[totalDays] };
                    day = stime.Date;
                    for (int i = 0; i < totalDays; i++)
                    {
                        oneDayData = query.Where(d => d.Plan.PlanDate == day && d.Doctor.HospitalId == hosp.Id).ToList();
                        reportItem.data[i] = oneDayData.Count();
                        view.XDates[i] = day.ToString("M-d");
                        day = day.AddDays(1);
                    }
                    view.Series.Add(reportItem);
                }
            }
            else
            {
                IList<TBReferral> oneDayData = null;
                var query = await _referralRepository.FindAsync(d => d.ReferralStatus == ReferralStatus.Confirm && d.RequestDate >= stime && d.RequestDate <= etime && d.Id == d.MedicalHistory.NewestReferralId);

                //if (request.HospitalId > 0)
                //{
                //    hosps = hosps.Where(d => d.Id == request.HospitalId).ToList();
                //}
                //view.Hospitals = hosps.Select(d => d.Name).ToArray();
                foreach (var hosp in hosps)
                {
                    reportItem = new ReportItemView { name = hosp.Name, type = "bar", data = new int[totalDays] };
                    day = stime.Date;
                    for (int i = 0; i < totalDays; i++)
                    {
                        oneDayData = query.Where(d => d.RequestDate == day && d.Doctor.HospitalId == hosp.Id).ToList();
                        reportItem.data[i] = oneDayData.Count();
                        view.XDates[i] = day.ToString("M-d");
                        day = day.AddDays(1);
                    }
                    view.Series.Add(reportItem);
                }
            }
            return view;
        }

        public async Task<PagedData<ReportListView>> GetConsultationListInReport(ReportQueryRequest request)
        {
            DateTime stime = Convert.ToDateTime(request.STime);
            DateTime etime = Convert.ToDateTime(request.ETime);
            var query = await _consultationRepository.FindAsync(d => d.ConsultationStatus == ConsultationStatus.Complete && d.Plan.STime >= stime && d.Plan.ETime <= etime && d.Id == d.MedicalHistory.NewestConsultationId);
            if (request.HospitalId > 0)
            {
                query = query.Where(d => d.Id == request.HospitalId);
            }
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ReportListViews());
            return pageData;
        }


        public async Task<PagedData<ReportListView>> GetReferralListInReport(ReportQueryRequest request)
        {
            DateTime stime = Convert.ToDateTime(request.STime);
            DateTime etime = Convert.ToDateTime(request.ETime);
            var query = await _referralRepository.FindAsync(d => d.ReferralStatus == ReferralStatus.Confirm && d.RequestDate >= stime && d.RequestDate <= etime && d.Id == d.MedicalHistory.NewestReferralId);
            if (request.HospitalId > 0)
            {
                query = query.Where(d => d.Doctor.HospitalId == request.HospitalId);
            }
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ReportListViews());
            return pageData;
        }


        #endregion

    }
}
