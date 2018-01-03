using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Models
{
    public class DoctorListModel
    {
        public IList<SelectItem> HospitalList { get; set; }
    }
}
