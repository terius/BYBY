using BYBY.Infrastructure;

namespace BYBY.Services.Views
{
    public class LoginUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public RoleType RoleName { get; set; }


        public bool IsMasterDoctor { get; set; }

        public bool IsChildDoctor { get; set; }

        public int DoctorId { get; set; }

        public int HospitalId { get; set; }

    }
}