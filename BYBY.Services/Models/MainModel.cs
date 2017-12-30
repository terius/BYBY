using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class MainModel
    {
        public int ConsultationRequestCount { get; set; }
        public int ConsultationCancelCount { get; set; }
        public int ConsultationConfirmCount { get; set; }


        public int ReferralRequestCount { get; set; }
        public int ReferralCancelCount { get; set; }
        public int ReferralConfirmCount { get; set; }

        public IList<SelectItem> RoomList { get; set; }

        public int DoctorId { get; set; }

        public bool IsChildDoctor { get; set; }
        public RoleType RoleType { get; set; }
    }
}