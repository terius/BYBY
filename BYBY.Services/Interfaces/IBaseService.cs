using BYBY.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IBaseService
    {
        Task<IList<SelectItem>> GetLoginUserMasterHospitalList();


        bool IsMasterDoctor { get; }
    }
}
