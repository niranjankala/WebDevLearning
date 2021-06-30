using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Common
{
    public static class SiteConfig
    {
        // Avatar
        public static string AvatarWidth { get { return GetConfig("AvatarWidth"); } }
        public static string AvatarHeight { get { return GetConfig("AvatarHeight"); } }

        private static string GetConfig(string configName)
        {
            var configs = System.Configuration.ConfigurationManager.GetSection("appSettings") as NameValueCollection;
            return configs.GetValues(configName).SingleOrDefault().ToString();
        }

    }
}
