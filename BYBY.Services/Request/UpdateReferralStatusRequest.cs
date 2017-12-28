using BYBY.Infrastructure;

namespace BYBY.Services.Request
{
    public class UpdateReferralStatusRequest
    {
        public int Id { get; set; }

        public ReferralStatus Status { get; set; }

    }
}
