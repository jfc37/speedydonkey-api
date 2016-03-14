using System.Configuration;

namespace Common
{
    public interface IAppSettings
    {
        string GetWebsiteUrl();
        string GetApiUrl();
        string GetSetting(AppSettingKey key);
    }

    public class AppSettings : IAppSettings
    {
        public string GetWebsiteUrl()
        {
            var websiteUrl = ConfigurationManager.AppSettings.Get("WebsiteUrl");
            websiteUrl = "https://" + websiteUrl;
            return websiteUrl;
        }

        public string GetApiUrl()
        {
            var websiteUrl = ConfigurationManager.AppSettings.Get("ApiUrl");
            websiteUrl = "https://" + websiteUrl;
            return websiteUrl;
        }

        public string GetSetting(AppSettingKey key)
        {
            return ConfigurationManager.AppSettings.Get(key.ToString());
        }
    }

    public enum AppSettingKey
    {
        ApplicationName,

        MandrillApiKey,
        TestEmailAccount,
        UseRealEmail,
        AdminEmailWhitelist,
        FromEmail,
        ShouldSendEmail,
        AllowDatabaseDelete,

        PayPalUsername,
        PayPalPassword,
        PayPalSignature,
        PayPalMode,

        PoliAuthorisation,
        PoliInitiateUrl,

        AuthZeroRealIntegration,
        AuthZeroClientId,
        AuthZeroClientSecret,
        AuthZeroDomain,
        AuthZeroToken,
        AuthZeroConnection,
        AuthZeroApiKey,
        AuthZeroApiSecret
    }
}
