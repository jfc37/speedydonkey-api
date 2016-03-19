using System;
using Auth0.ManagementApi;
using Common;

namespace AuthZero.Domain.Proxies
{
    public static class AuthZeroManagementClientFactory
    {
        public static ManagementApiClient Create(IAppSettings appSettings)
        {
            var jwt = appSettings.GetSetting(AppSettingKey.AuthZeroToken);
            var api = new Uri($"https://{appSettings.GetSetting(AppSettingKey.AuthZeroDomain)}/api/v2");

            return new ManagementApiClient(jwt, api);
        }
    }
}