using System;
using System.Configuration;

namespace CardIndex.Helpers
{
    public static class ConfigHelper
    {
        public static int ItemPerPage { get { return Convert.ToInt32(ConfigurationManager.AppSettings["itemPerPage"]); } }
    }
}
