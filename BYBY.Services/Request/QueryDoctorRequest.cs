﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Request
{
    public class QueryDoctorRequest : PageQueryRequest
    {
        public string SearchKey { get; set; }
    }
}