using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models.Settings
{
    /// <summary>
    /// Complete set of settings
    /// </summary>
    public class CompleteSettings : IEntity, IDatabaseEntity
    {
        public CompleteSettings()
        {
            
        }

        public CompleteSettings(IEnumerable<SettingItem> settings)
        {
            Settings = settings;
        }

        public virtual int Id { get; set; }
        public virtual IEnumerable<SettingItem> Settings { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Settings));
        }
    }
}
