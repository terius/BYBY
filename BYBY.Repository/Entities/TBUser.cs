using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Repository.Entities
{
    public class TBUser :BaseEntity<int>
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }


        [Required]
        [StringLength(18)]
        public string SFZ { get; set; }

    }
}
