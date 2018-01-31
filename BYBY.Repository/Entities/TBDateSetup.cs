using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBDateSetup : BaseEntity<int>
    {
     
        public DateTime STime { get; set; }
        public DateTime ETime { get; set; }

        public int HospitalId { get; set; }


        public int DefaultPeople { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }
    }
}
