using System;
using System.Collections.Generic;
using System.Linq;
using Models.Settings;
using Validation.Rules;

namespace Validation.Validators.Settings
{
    public class IsLogoSettingAValidUrl : IRule
    {
        private readonly IEnumerable<SettingItem> _settings;

        public IsLogoSettingAValidUrl(IEnumerable<SettingItem> settings)
        {
            _settings = settings;
        }

        public bool IsValid()
        {
            var attemptedUrl = _settings.Single(x => x.Name == SettingTypes.Logo).Value;

            Uri result;
            return Uri.TryCreate(attemptedUrl, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}