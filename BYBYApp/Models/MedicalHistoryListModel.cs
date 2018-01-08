using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBYApp.Models
{
    public class MedicalHistoryListModel
    {
        public IList<SelectItem> HospitalList { get; set; }
        public IList<SelectItem> DoctorList { get; set; }

        public IList<SelectItem> MasterHospitalList { get; set; }

        public string DefaultMasterHospitalId { get; set; }
    }
}