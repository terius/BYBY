using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Repository.Entities
{
    public class TBPlan : BaseEntity<int>
    {
        //public int DateSetupId { get; set; }

        public TBPlan()
        {
            this.Consultations = new HashSet<TBConsultation>();
        }

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


        public virtual ICollection<TBConsultation> Consultations { get; set; }

        [NotMapped]
        public IEnumerable<TBConsultation> ValidConsultations
        {
            get
            {
                return Consultations.Where(d => d.ConsultationStatus != ConsultationStatus.Cancel && d.ConsultationStatus != ConsultationStatus.Complete);

            }
        }

        protected override void Validate()
        {
            if (dbaction == DBAction.Delete)
            {
                if (ValidConsultations.Count() > 0)
                {
                    AddError(ErrorType.NotEmpty, "当前排班含有未完成的会诊，禁止删除");
                }
            }
            base.Validate();
        }
    }
}
