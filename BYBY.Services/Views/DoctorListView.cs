using BYBY.Infrastructure;
using System;

namespace BYBY.Services.Views
{
    public class DoctorListView
    {
        public int Id { get; set; }

        public string Name { get; set; }



        public string JobTitle { get; set; }


        public string HospitalName { get; set; }

        public int HospitalId { get; set; }


       
      //  public string IsMasterDoctor { get; set; }

        public Sex Sex { get; set; }

        public string SexText { get; set; }
        public string Age { get; set; }

        public string Birthday { get; set; }

        public string Remark { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string Department { get; set; }

    }
}
