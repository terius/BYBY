using System;

namespace BYBY.Services.Views
{
    public class DoctorListView
    {
        public int Id { get; set; }

        public string Name { get; set; }


     
        public string JobTitle { get; set; }
        
      
        public string Hospital { get; set; }

       
        /// <summary>
        /// 是否为母院医生
        /// </summary>
        public string IsMasterDoctor { get; set; }

      

        public string Sex { get; set; }
        public string Age { get; set; }
    }
}
