using System.Collections.Generic;
using Common.Extensions;

namespace SpeedyDonkeyApi.Models.Settings
{
    /// <summary>
    /// Complete set of settings
    /// </summary>
    public class CompleteSettingsModel
    {
        public IEnumerable<SettingItemModel> Settings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteSettingsModel"/> class.
        /// </summary>
        public CompleteSettingsModel()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteSettingsModel"/> class.
        /// </summary>
        /// <param name="settingItem">The setting item.</param>
        public CompleteSettingsModel(SettingItemModel settingItem)
        {
            Settings = new[] {settingItem};
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Settings));
        }
    }
}