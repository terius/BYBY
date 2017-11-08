using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBUser : BaseEntity<int>, IUser<int>
    {
      
        public TBUser()
        {
            this.UserRoles = new HashSet<TBUserRole>();
        }


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

    }
}
