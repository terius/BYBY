using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBMedicalHistoryImage : BaseEntity<int>
    {

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(200)]
        [Required]
        public string FilePath { get; set; }

      
        public int TBMedicalHistoryId { get; set; }


        [ForeignKey("TBMedicalHistoryId")]
        public virtual TBMedicalHistory TBMedicalHistory { get; set; }

    }
}
