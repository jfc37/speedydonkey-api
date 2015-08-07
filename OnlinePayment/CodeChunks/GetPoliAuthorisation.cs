using System;
using Common;
using Data.CodeChunks;

namespace OnlinePayments.CodeChunks
{
    public class GetPoliAuthorisation : ICodeChunk<string>
    {
        private readonly IAppSettings _appSettings;

        public GetPoliAuthorisation(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string Do()
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_appSettings.GetSetting(AppSettingKey.PoliAuthorisation)));
        }
    }
}