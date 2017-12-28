using System;

namespace BYBY.Repository.Entities
{
    public class TBDateSetup : BaseEntity<int>
    {
     
        public DateTime STime { get; set; }
        public DateTime ETime { get; set; }

       
        public int DefaultPeople { get; set; }
    }
}
