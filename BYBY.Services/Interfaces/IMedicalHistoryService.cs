using BYBY.Services.Request;
using BYBY.Services.Response;
using BYBY.Services.Views;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IMedicalHistoryService
    {
        Task<PagedData<MedicalHistoryListView>> GetMedicalHistoryList(PageQueryRequest request);
    }
}
