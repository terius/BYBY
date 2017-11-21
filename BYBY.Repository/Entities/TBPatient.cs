using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBPatient :BaseEntity<int>
    {
        public TBPatient()
        {
            this.MaleMedicalHistorys = new HashSet<TBMedicalHistory>();
            this.FeMaleMedicalHistorys = new HashSet<TBMedicalHistory>();
        }

        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        public DateTime Birthday { get; set; }

  
        public int? Age { get; set; }
        [Required]
        public CardType CardType { get; set; }
        [Required]
        [StringLength(50)]
        public string CardNo { get; set; }

        public MaritalStatus MaritalStatus { get; set; }


        public Sex Sex { get; set; }


        public virtual ICollection<TBMedicalHistory> MaleMedicalHistorys { get; set; }
        public virtual ICollection<TBMedicalHistory> FeMaleMedicalHistorys { get; set; }
    }
}
