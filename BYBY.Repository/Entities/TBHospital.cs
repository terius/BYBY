using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBHospital : BaseEntity<int>
    {
        public TBHospital()
        {
            Doctors = new HashSet<TBDoctor>();
            Rooms = new HashSet<TBConsultationRoom>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsMaster { get; set; }



        [StringLength(500)]
        public string Remark { get; set; }



        public virtual ICollection<TBDoctor> Doctors { get; set; }


        public int? ParentHospitalId { get; set; }


        [ForeignKey("ParentHospitalId")]
        public virtual TBHospital ParentHospital { get; set; }


        public virtual ICollection<TBConsultationRoom> Rooms { get; set; }


    }
}
