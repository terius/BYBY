using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBUser : BaseEntity<int>, IUser<int>
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }


        [Required]
        [StringLength(30)]
        public string Name { get; set; }


        [Required]
        [StringLength(32)]
        public string Password { get; set; }



        public DateTime? LastLoginTime { get; set; }

    }
}
