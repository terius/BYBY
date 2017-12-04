using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBRole : BaseEntity<string>, IRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string DisplayName { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}
