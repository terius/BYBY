using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBRoleModule : BaseEntity<string>
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string ModuleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual TBRole Role { get; set; }

        [ForeignKey("ModuleId")]
        public virtual TBModule Module { get; set; }


        public int? OrderBy { get; set; }

    }
}
