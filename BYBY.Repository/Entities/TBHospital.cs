using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BYBY.Repository.Entities
{
    public class TBHospital : BaseEntity<int>
    {
        public TBHospital()
        {
            Doctors = new HashSet<TBDoctor>();
            Rooms = new HashSet<TBConsultationRoom>();
            MasterHospitals = new HashSet<TBMasterHospital>();
            ChildHospitals = new HashSet<TBMasterHospital>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsMaster { get; set; }



        [StringLength(500)]
        public string Remark { get; set; }



        public virtual ICollection<TBDoctor> Doctors { get; set; }


        //public int? ParentHospitalId { get; set; }


        //[ForeignKey("ParentHospitalId")]
        //public virtual TBHospital ParentHospital { get; set; }


        public virtual ICollection<TBConsultationRoom> Rooms { get; set; }


        public virtual ICollection<TBMasterHospital> MasterHospitals { get; set; }


        public virtual ICollection<TBMasterHospital> ChildHospitals { get; set; }

        public IEnumerable<ICollection<TBPlan>> GetPlanStartToday()
        {
            var dtNow = DateTime.Now.Date;
            var plans = this.Rooms.Where(d => d.Plans.Any(f => f.STime >= dtNow)).Select(d => d.Plans);
            plans = plans.OrderBy(d => d.OrderBy(f => f.STime)).ToList();
            return plans;
        }


    }
}
