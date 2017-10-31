namespace Sceneray.CSCenter.AppService.SSUser
{
    public class UserFactory
    {
        private static UserManager _user;

        public static void InitializeLogFactory(UserManager user)
        {
            _user = user;
        }

        public static UserManager GetUserManager()
        {
            return _user;
        }
    }
}
