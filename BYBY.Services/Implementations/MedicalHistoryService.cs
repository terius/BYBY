using BYBY.Infrastructure.Domain;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Threading.Tasks;
using BYBY.Services;
using System.Linq;

namespace BYBY.Services.Implementations
{
    public class MedicalHistoryService : BaseService, IMedicalHistoryService
    {

        readonly IRepository<TBMedicalHistory, int> _repository;

        public MedicalHistoryService(IRepository<TBMedicalHistory, int> repository)
        {
            _repository = repository;
        }


        public async Task<PagedData<MedicalHistoryListView>> GetMedicalHistoryList(PageQueryRequest request)
        {
            var data = await _repository.FindAsync(d => d.MedicalHistoryNo == "9999");
            data = data.OrderBy(d => d.Id);
            var pageData = PageQuery(data, request, d => d.C_To_MedicalHistoryListViews());
            return pageData;

        }


    }
}
