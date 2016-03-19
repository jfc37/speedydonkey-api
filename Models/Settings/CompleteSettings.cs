using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models.Settings
{
    /// <summary>
    /// Complete set of settings
    /// </summary>
    public class CompleteSettings : DatabaseEntity
    {
        public CompleteSettings()
        {
            
        }

        public CompleteSettings(IEnumerable<SettingItem> settings)
        {
            Settings = settings;
        }
        
        public virtual IEnumerable<SettingItem> Settings { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Settings));
        }
    }
}
