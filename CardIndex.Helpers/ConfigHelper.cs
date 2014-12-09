using System.Configuration;

namespace CardIndex.Helpers
{
    public static class ConfigHelper
    {
        public static string AccountApiUrl { get { return ConfigurationManager.AppSettings["BookApiUrl"]; } }
    }
}
