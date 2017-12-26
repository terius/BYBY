using BYBY.Infrastructure;

namespace BYBY.Services.Request
{
    public class UpdateConsultationStatusRequest
    {
        public int Id { get; set; }

        public ConsultationStatus Status { get; set; }

    }
}
