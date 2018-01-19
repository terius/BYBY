namespace BYBY.Services.Request
{
    public class ReportQueryRequest: PageQueryRequest
    {
        public int Type { get; set; }
        public int HospitalId { get; set; }

        public string STime { get; set; }

        public string ETime { get; set; }
    }
}
