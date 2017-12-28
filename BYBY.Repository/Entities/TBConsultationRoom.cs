using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBConsultationRoom : BaseEntity<int>
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Pic { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}
