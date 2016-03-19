using System.Collections.Generic;
using System.Linq;
using Models.Settings;
using Validation.Rules;

namespace Validation.Validators.Settings
{
    public class AreAllSettingNamesValid : IRule
    {
        private readonly IEnumerable<SettingItem> _settings;

        public AreAllSettingNamesValid(IEnumerable<SettingItem> settings)
        {
            _settings = settings;
        }

        public bool IsValid()
        {
            return _settings.All(x => x.Name != SettingTypes.Invalid);
        }
    }
}