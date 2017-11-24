using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBHospital : BaseEntity<int>
    {
        public TBHospital()
        {

        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsMaster { get; set; }



        [StringLength(500)]
        public string Remark { get; set; }



    }
}
