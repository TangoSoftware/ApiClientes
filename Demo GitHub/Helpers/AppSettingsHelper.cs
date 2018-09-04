using System.Configuration;

namespace Demo_GitHub.Helpers
{
    public static class AppSettingsHelper
    {
        public static string IdCliente { get { return ConfigurationManager.AppSettings.Get("IdCliente"); } }

        public static string Token { get { return ConfigurationManager.AppSettings.Get("Token"); } }

        public static string UrlApi { get { return ConfigurationManager.AppSettings.Get("UrlAPICompJson"); } }        
    }
}