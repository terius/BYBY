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
        readonly IUnitOfWork _unitOfWork;

        public MedicalHistoryService(IRepository<TBMedicalHistory, int> repository,
            IRepository<TBMedicalDetail, int> medicalDetailRepository,
            IRepository<TBConsultation, int> consultationRepository,
            IRepository<TBReferral, int> referralRepository,
            IRepository<TBPatient, int> patientRepository,
            IRepository<TBMedicalHistoryImage, int> imageRepository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _medicalDetailRepository = medicalDetailRepository;
            _consultationRepository = consultationRepository;
            _referralRepository = referralRepository;
            _patientRepository = patientRepository;
            _imageRepository = imageRepository;
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
            model.Images = Mapper.Map<List<MedicalHistoryImageRequest>>(info.TBMedicalHistoryImages.OrderBy(d=>d.Id));
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
            var icount = await _medicalDetailRepository.FindCount(d => d.PatientId == patientId);
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
               
                filePath= HttpContext.Current.Server.MapPath(filePath);
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

            //更新病历会诊状态
            var mdInfo = await _repository.GetAsync(request.TBMedicalHistoryId);
            mdInfo.ConsultationStatus = ConsultationStatus.Requesting;
            mdInfo.ModifyUserName = loginUserName;
            await _repository.UpdateAsync(mdInfo);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

        public async Task<EmptyResponse> UpdateConsultationToCancel(ConsultationCancelRequest cancelRequest)
        {
            var loginUserName = GetLoginUserName();
            var MHInfo = await _repository.GetAsync(cancelRequest.MHId);
            MHInfo.ConsultationStatus = ConsultationStatus.Cancel;
            MHInfo.ModifyUserName = loginUserName;
            await _repository.UpdateAsync(MHInfo);

            var cInfo = await _consultationRepository.GetAsync(MHInfo.Consultations.OrderByDescending(d => d.Id).First().Id);
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


            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ConsultationListViews());
            return await Task.FromResult(pageData);

        }


        public async Task<ConsultationDetailModel> GetConsultationDetail(int id)
        {
            var info = await _consultationRepository.GetAsync(id);
            var model = info.C_To_ConsultationDetailModel();
            return model;
        }


        public async Task<EmptyResponse> SaveConsultationRecord(ConsultationRecordEditRequest request)
        {
            var editInfo = await _consultationRepository.GetAsync(request.MyId);
            editInfo = Mapper.Map(request, editInfo);
            editInfo.ModifyUserName= editInfo.RecordUser = GetLoginUserName();
            editInfo.RecordTime = DateTime.Now;
            await _consultationRepository.UpdateAsync(editInfo);
            int rs = _unitOfWork.Commit();
            return rs > 0 ? EmptyResponse.CreateSuccess("保存成功") : EmptyResponse.CreateError("保存失败");
        }

            #endregion


            #region 转诊模块
            public async Task<EmptyResponse> SaveReferralAdd(ReferralAddRequest request)
        {
            var loginUserName = GetLoginUserName();
            var info = Mapper.Map<TBReferral>(request);
            info.AddUserName = loginUserName;
            info.ReferralStatus = ReferralStatus.Requesting;
            await _referralRepository.InsertAsync(info);

            //更新病历会诊状态
            var mdInfo = await _repository.GetAsync(request.TBMedicalHistoryId);
            mdInfo.ReferralStatus = ReferralStatus.Requesting;
            mdInfo.ModifyUserName = loginUserName;
            await _repository.UpdateAsync(mdInfo);
            int rs = _unitOfWork.Commit();
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


            //   var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            var pageData = PageQuery(query.OrderBy(d => d.Id), request, d => d.C_To_ReferralListViews());
            return await Task.FromResult(pageData);

        }

        #endregion

    }
}
