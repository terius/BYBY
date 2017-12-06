using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBRole : BaseEntity<string>, IRole
    {
        public TBRole()
        {
            this.RoleModules = new HashSet<TBRoleModule>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string DisplayName { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }


        public virtual ICollection<TBRoleModule> RoleModules { get; set; }
    }
}
