using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class PatientStatisticsModel
    {
        public IList<SelectItem> ChildHospitalList { get; set; }
        public int MyHospitalId { get; set; }
    }
}
