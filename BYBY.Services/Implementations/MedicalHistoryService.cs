﻿using BYBY.Infrastructure.Domain;
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

namespace BYBY.Services.Implementations
{
    public class MedicalHistoryService : BaseService, IMedicalHistoryService
    {

        readonly IRepository<TBMedicalHistory, int> _repository;
        readonly IUnitOfWork _unitOfWork;

        public MedicalHistoryService(IRepository<TBMedicalHistory, int> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
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
            return model;
        }


        public async Task<IList<MedicalDetailRequest>> GetMedicalDetails(TBPatient patient)
        {
            var rs = Task.Run(() =>
           {
               var view = patient.MedicalDetails.C_To_MedicalDetailRequests();
              
               return view;
           }).Result;


            return await Task.FromResult(rs);
        }


    }
}
