using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBUserRole : BaseEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual TBUser User { get; set; }
        [ForeignKey("RoleId")]
        public virtual TBRole Role { get; set; }
    }
}
