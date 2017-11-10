namespace BYBY.Infrastructure
{
    public class Configer
    {
        public static readonly string LogerName = System.Configuration.ConfigurationManager.AppSettings["LoggerName"];
    }
}
