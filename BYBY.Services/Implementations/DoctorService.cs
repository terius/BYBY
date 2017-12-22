using BYBY.Infrastructure;
using BYBY.Infrastructure.Domain;
using BYBY.Infrastructure.UnitOfWork;
using BYBY.Repository.Entities;
using BYBY.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BYBY.Services.Implementations
{
    public class DoctorService : BaseService, IDoctorService
    {

        readonly IRepository<TBDoctor, int> _repository;
        readonly IRepository<TBHospital, int> _hospitalRepository;
        readonly IUnitOfWork _unitOfWork;

        public DoctorService(IRepository<TBDoctor, int> repository,
            IRepository<TBHospital, int> hospitalRepository,
        IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _hospitalRepository = hospitalRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SelectItem>> GetDoctorChildHospitals()
        {
            var doctor = await GetLoginDoctorInfo();
            if (doctor.IsMasterDoctor)
            {
                var hospitalId = doctor.Hospital.Id;
                var list = await _hospitalRepository.FindAsync(d => d.ParentHospitalId == hospitalId);
                return list.ConvertTo_SelectItem();
            }
            return new List<SelectItem>();
        }

        public async Task<IList<SelectItem>> GetGroupHospitals()
        {
            IList<SelectItem> pList = new List<SelectItem>();
            var masterList = await _hospitalRepository.FindAsync(d => d.IsMaster == true);
            SelectItem hosp;
            foreach (var item in masterList)
            {
                hosp = new SelectItem { id = item.Id.ToString(), text = item.Name + "-母院" };
                var childList = await _hospitalRepository.FindAsync(d => d.ParentHospitalId == item.Id);
                hosp.children = childList.ConvertTo_SelectItem();
                hosp.children.Insert(0, new SelectItem { id = item.Id.ToString(), text = item.Name });
                pList.Add(hosp);
            }
            return pList;
        }

    }
}
