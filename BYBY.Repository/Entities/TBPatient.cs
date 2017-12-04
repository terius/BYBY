using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBPatient :BaseEntity<int>
    {
        public TBPatient()
        {
            this.MaleMedicalHistorys = new HashSet<TBMedicalHistory>();
            this.FeMaleMedicalHistorys = new HashSet<TBMedicalHistory>();
            this.MedicalDetails = new HashSet<TBMedicalDetail>();
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

        [StringLength(100)]
        public string ContactPhone { get; set; }

        public int? NationaId { get; set; }

        public int? JobId { get; set; }

        public int? EthnicId { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        [StringLength(100)]
        public string  HouseholdAddress { get; set; }


        /// <summary>
        /// 籍贯
        /// </summary>
        [StringLength(50)]
        public string NativePlace { get; set; }
        

        public virtual ICollection<TBMedicalHistory> MaleMedicalHistorys { get; set; }
        public virtual ICollection<TBMedicalHistory> FeMaleMedicalHistorys { get; set; }


        public virtual ICollection<TBMedicalDetail> MedicalDetails { get; set; }

        [ForeignKey("NationaId")]
        public virtual TBNation Nationality { get; set; }

        [ForeignKey("JobId")]
        public virtual TBJob Job { get; set; }

        [ForeignKey("EthnicId")]
        public virtual TBEthnic Ethnic { get; set; }
    }
}
