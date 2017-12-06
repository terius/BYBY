using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBModule : BaseEntity<string>
    {
     

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string Text { get; set; }

        [StringLength(100)]
        public string Path { get; set; }

        [StringLength(128)]
        public string ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual TBModule ParentModule { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }


        public bool  IsMenu { get; set; }

        public int? OrderBy { get; set; }
    }
}
