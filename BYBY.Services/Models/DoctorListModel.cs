using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class DoctorListModel :BaseModel
    {
        public IList<SelectItem> HospitalList { get; set; }

       
    }
}
