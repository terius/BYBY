using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBNation : BaseEntity<int>
    {
      
        public TBNation()
        {
         
        }


        [Required]
        [StringLength(20)]
        public string Chinese { get; set; }


        [Required]
        [StringLength(30)]
        public string English { get; set; }


      

    }
}
