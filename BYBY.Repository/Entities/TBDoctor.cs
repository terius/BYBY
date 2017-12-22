using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBDoctor : BaseEntity<int>
    {
      
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

 
        [StringLength(50)]
        public string JobTitle { get; set; }


        [Required]
        [StringLength(128)]
        public string UserId { get; set; }


        public int HospitalId { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }

        [ForeignKey("UserId")]
        public virtual TBUser User { get; set; }

        /// <summary>
        /// 是否为母院医生
        /// </summary>
        public bool IsMasterDoctor { get; set; }
    }
}
