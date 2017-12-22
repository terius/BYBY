namespace BYBY.Services.Request
{
    public class ConsultationQueryRequest: PageQueryRequest
    {
        public bool IsAll { get; set; }
        public bool IsRequest { get; set; }
        public bool IsCancel { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsComplete { get; set; }

        public string SearchKey { get; set; }

        public string Stime { get; set; }

        public string Etime { get; set; }

        public int? HospitalId { get; set; }
    }
}
