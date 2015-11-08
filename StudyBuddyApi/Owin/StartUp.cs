using System.Configuration;
using System.Web.Http;
using Common;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using SpeedyDonkeyApi.Owin;

[assembly: OwinStartup(typeof(StartUp))]
namespace SpeedyDonkeyApi.Owin
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config, app);

            ConfigAuthZero(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigAuthZero(IAppBuilder app)
        {
            var appSettings = new AppSettings();
            var issuer = "https://" + appSettings.GetSetting(AppSettingKey.AuthZeroDomain) + "/";
            var audience = appSettings.GetSetting(AppSettingKey.AuthZeroClientId);
            var secret = TextEncodings.Base64.Encode(TextEncodings.Base64Url.Decode(appSettings.GetSetting(AppSettingKey.AuthZeroClientSecret)));

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] {audience},
                IssuerSecurityTokenProviders = new[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                }
            });
        }
    }
}