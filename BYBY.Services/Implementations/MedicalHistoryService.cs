using BYBY.Infrastructure.Domain;
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

        public MedicalHistoryService(IRepository<TBMedicalHistory, int> repository)
        {
            _repository = repository;
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


    }
}
