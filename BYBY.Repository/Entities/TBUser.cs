using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BYBY.Repository.Entities
{
    public class TBUser : BaseEntity<string>, IUser
    {

        public TBUser()
        {
            //  this.Id = Guid.NewGuid().ToString();
            this.UserRoles = new HashSet<TBUserRole>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }


        [Required]
        [StringLength(20)]
        public string UserName { get; set; }


        [Required]
        [StringLength(30)]
        public string Name { get; set; }


        [Required]
        [StringLength(200)]
        public string Password { get; set; }



        public DateTime? LastLoginTime { get; set; }

        public virtual ICollection<TBUserRole> UserRoles { get; set; }

        protected override void Validate()
        {
            base.Validate();

        }

        public IList<TBModule> GetModules(string roleName)
        {
            return UserRoles.FirstOrDefault(d => d.Role.Name == roleName).Role.RoleModules.Select(d => d.Module).OrderBy(d => d.OrderBy).ToList();
        }

    }
}
