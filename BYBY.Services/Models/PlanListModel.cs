using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class PlanListModel
    {
        public IList<SelectItem> DoctorList { get; set; }
        public IList<SelectItem> RoomList { get; set; }

      //  public IList<SelectItem> HospitalList { get; set; }
    }
}
