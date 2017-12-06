namespace BYBY.Services.Account
{
    public class RoleFactory
    {
        private static RoleManager _role;

        public static void InitializeLogFactory(RoleManager role)
        {
            _role = role;
        }

        public static RoleManager GetRoleManager()
        {
            return _role;
        }
    }
}
