using System.Configuration;

namespace Common
{
    public interface IAppSettings
    {
        string GetWebsiteUrl();
    }

    public class AppSettings : IAppSettings
    {
        public string GetWebsiteUrl()
        {
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            return websiteUrl;
        }
    }
}
