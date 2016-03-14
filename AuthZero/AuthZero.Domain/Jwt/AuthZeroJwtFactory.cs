using System;
using System.Collections.Generic;
using Common;
using Jose;

namespace AuthZero.Domain.Jwt
{
    public static class AuthZeroJwtFactory
    {
        public static string Create(IAppSettings appSettings)
        {
            var secret = appSettings.GetSetting(AppSettingKey.AuthZeroApiSecret);
            var domain = appSettings.GetSetting(AppSettingKey.AuthZeroDomain);
            var clientId = appSettings.GetSetting(AppSettingKey.AuthZeroApiKey);

            byte[] secretKey = Base64UrlDecode(secret);
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddHours(10);

            var payload = new Dictionary<string, object>()
            {
                {"iss", $"https://{domain}/"},
                {"aud", clientId},
                {"sub", "anonymous"},
                {"iat", ToUnixTime(issued).ToString()},
                {"exp", ToUnixTime(expire).ToString()}
            };

            string token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

            return token;
        }

        /// <remarks>
        /// Take from http://stackoverflow.com/a/33113820
        /// </remarks>
        static byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        static long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
