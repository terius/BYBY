using System;

namespace BYBY.Services.Request
{
    public class ReferralAddRequest
    {
        public int TBMedicalHistoryId { get; set; }
        public int HospitalId { get; set; }
 

        public DateTime RequestDate { get; set; }

        public string Remark { get; set; }

        public int DoctorId { get; set; }
    }
}
