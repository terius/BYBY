using System.Threading.Tasks;

namespace BYBY.Services.Interfaces
{
    public interface IBaseService
    {
        Task<int> GetDoctorMasterHospitalId();
    }
}
