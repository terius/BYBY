using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBJob : BaseEntity<int>
    {
      
        [Required]
        [StringLength(20)]
        public string Name { get; set; }



    }
}
