using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBYApp.Models
{
    public class ReferralListModel
    {
        public IList<SelectItem> HospitalList { get; set; }

        public int MasterHospitalId { get; set; }
    }
}