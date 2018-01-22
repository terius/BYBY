using System;
using System.Collections.Generic;

namespace BYBY.Services.Views
{
    public class PlanListViewByHospital
    {
        public PlanListViewByHospital()
        {
            this.DateViews = new List<DateSetupViewByHospital>();
            this.WeekTitles = new List<string>();
        }
        public IList<DateSetupViewByHospital> DateViews { get; set; }
        public IList<string> WeekTitles { get; set; }
        public string DateSelect { get; set; }
       
        public string EndDate { get; set; }
    }

    public class DateSetupViewByHospital
    {
        public DateSetupViewByHospital()
        {
            this.DayPlans = new List<DayPlan>();
        }
        public string STime { get; set; }

        public string ETime { get; set; }

        public IList<DayPlan> DayPlans { get; set; }
    }

    public class DayPlan
    {
        public DayPlan()
        {
            this.PlanViews = new List<PlanView>();
        }
        public IList<PlanView> PlanViews { get; set; }

  
        public bool IsCanSelect { get; set; }
    }


}
