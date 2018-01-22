using System;
using System.Collections.Generic;

namespace BYBY.Services.Views
{
    public class PlanListView
    {
        public PlanListView()
        {
            this.DateViews = new List<DateSetupView>();
            this.WeekTitles = new List<string>();
        }
        public IList<DateSetupView> DateViews { get; set; }
        public IList<string> WeekTitles { get; set; }
        public string DateSelect { get; set; }
       
        public string EndDate { get; set; }
    }

    public class DateSetupView
    {
        public DateSetupView()
        {
            this.OneDayPlans = new List<PlanView>();
        }
        public string STime { get; set; }

        public string ETime { get; set; }

        public IList<PlanView> OneDayPlans { get; set; }
        //public PlanView Day2 { get; set; }
        //public PlanView Day3 { get; set; }
        //public PlanView Day4 { get; set; }
        //public PlanView Day5 { get; set; }
        //public PlanView Day6 { get; set; }
        //public PlanView Day7 { get; set; }
    }

    public class PlanView
    {
        public int Id { get; set; }
        public string STime { get; set; }

        public string ETime { get; set; }

        public int DoctorId { get; set; }
        public string PlanDate { get; set; }

        public int RoomId { get; set; }

        public int People { get; set; }

        public string RoomName { get; set; }

        public string HospitalName { get; set; }



        public string DoctorName { get; set; }

        public int ConsultationCount { get; set; }

        public IList<PlanConsultationView> ConsultationList { get; set; }

        public bool IsFull { get; set; }
    }

    public class PlanConsultationView
    {
        public string Hospital { get; set; }
        public string Doctor { get; set; }
        public string Female { get; set; }
        public string Male { get; set; }
        public string Status { get; set; }

        public int Id { get; set; }
    }

  
  
}
