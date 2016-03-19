using System.Collections.Generic;
using System.Linq;
using Models.Settings;
using Validation.Rules;

namespace Validation.Validators.Settings
{
    public class AreNoDuplicateSettings : IRule
    {
        private readonly IEnumerable<SettingItem> _settings;

        public AreNoDuplicateSettings(IEnumerable<SettingItem> settings)
        {
            _settings = settings;
        }

        public bool IsValid()
        {
            return _settings.Select(x => x.Name).Distinct().Count() == _settings.Count();
        }
    }
}