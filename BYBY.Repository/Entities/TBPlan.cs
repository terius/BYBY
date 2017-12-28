using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Repository.Entities
{
    public class TBPlan: BaseEntity<int>
    {
        //public int DateSetupId { get; set; }

        public int DoctorId { get; set; }
        public DateTime PlanDate { get; set; }

        public int RoomId { get; set; }

        public int People { get; set; }

        public DateTime STime { get; set; }

        public DateTime ETime { get; set; }


        //[ForeignKey("DateSetupId")]
        //public virtual TBDateSetup DateSetup { get; set; }

        [ForeignKey("DoctorId")]
        public virtual TBDoctor Doctor { get; set; }

        [ForeignKey("RoomId")]
        public virtual TBConsultationRoom Room { get; set; }

    }
}
