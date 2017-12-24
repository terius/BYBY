using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BYBYApp.Models
{
    public class ConsultationListModel
    {
        public IList<SelectItem> HospitalList { get; set; }

        public IList<SelectItem> MotherHospitalList { get; set; }
    }
}