using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Request
{
    public class ConsultationAddRequest
    {
        public int TBMedicalHistoryId { get; set; }
        public int HospitalId { get; set; }
        public int DoctorId { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime STime { get; set; }

        public DateTime ETime { get; set; }

        public string Remark { get; set; }
    }
}
