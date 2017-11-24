using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Request
{
    public class MedicalHistoryListSearchRequest : PageQueryRequest
    {
        public int HospitalId { get; set; }
        public string SearchKey { get; set; }

        public DateTime? STime { get; set; }
        public DateTime? ETime { get; set; }
    }
}
