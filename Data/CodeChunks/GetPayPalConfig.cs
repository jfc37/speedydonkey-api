using System.Collections.Generic;
using Common;

namespace Data.CodeChunks
{
    /// <summary>
    /// Gets pay pal configuration for express checkout api call
    /// </summary>
    public class GetPayPalConfig : ICodeChunk<Dictionary<string, string>>
    {
        private readonly IAppSettings _appSettings;

        public GetPayPalConfig(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public Dictionary<string, string> Do()
        {
            var paypalConfig = new Dictionary<string, string>();
            paypalConfig.Add("account1.apiUsername", _appSettings.GetSetting(AppSettingKey.PayPalUsername));
            paypalConfig.Add("account1.apiPassword", _appSettings.GetSetting(AppSettingKey.PayPalPassword));
            paypalConfig.Add("account1.apiSignature", _appSettings.GetSetting(AppSettingKey.PayPalSignature));
            paypalConfig.Add("mode", _appSettings.GetSetting(AppSettingKey.PayPalMode));

            return paypalConfig;
        }
    }
}