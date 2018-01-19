using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Views
{
    public class ReportView
    {
        public ReportView()
        {
            this.Series = new List<ReportItemView>();
        }
        public int Type { get; set; }
        public string[] Hospitals { get; set; }
        public string[] XDates { get; set; }
        public IList<ReportItemView> Series { get; set; }
    }

    public class ReportItemView
    {
        public string name { get; set; }
        public string type { get; set; }

        public int[] data { get; set; }
    }
}
