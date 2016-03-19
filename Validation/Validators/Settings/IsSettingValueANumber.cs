using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Models.Settings;
using Validation.Rules;

namespace Validation.Validators.Settings
{
    public class IsSettingValueANumber : IRule
    {
        private readonly IEnumerable<SettingItem> _settings;
        private readonly SettingTypes _settingType;

        public IsSettingValueANumber(IEnumerable<SettingItem> settings, SettingTypes settingType)
        {
            _settings = settings;
            _settingType = settingType;
        }

        public bool IsValid()
        {
            var value = _settings.Single(x => x.Name == _settingType).Value;

            return value.IsInt();
        }
    }
}