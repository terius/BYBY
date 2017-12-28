using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Request
{
    public class AddDateSetupRequest
    {
        public string STime { get; set; }
        public string ETime { get; set; }

        public int DefaultPeople { get; set; }
    }
}
