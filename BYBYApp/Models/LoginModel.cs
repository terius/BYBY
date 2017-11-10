namespace BYBYApp.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ValidCode { get; set; }

        public int RoleId { get; set; }
    }
}