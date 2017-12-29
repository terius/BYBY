using BYBY.Infrastructure;
using System;

namespace BYBY.Services.Request
{
    public class PlanQueryRequest
    {
        public string DateSelect { get; set; }

        public WeekSelect WeekSelect { get; set; }

        public int RoomId { get; set; }

        public int DoctorId { get; set; }
    }
}
