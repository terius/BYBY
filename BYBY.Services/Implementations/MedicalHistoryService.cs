using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
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

        public async Task<MedicalHistoryEditRequest> GetEditData(int id)
        {
            var info = await _repository.GetAsync(id);
           // var view = 
        }


    }
}
