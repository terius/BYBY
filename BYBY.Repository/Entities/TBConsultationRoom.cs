using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBConsultationRoom : BaseEntity<int>
    {
        public TBConsultationRoom()
        {
            Plans = new HashSet<TBPlan>();
        }


        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Pic { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

       
        public int HospitalId { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }

        public virtual ICollection<TBPlan> Plans { get; set; }
    }
}
