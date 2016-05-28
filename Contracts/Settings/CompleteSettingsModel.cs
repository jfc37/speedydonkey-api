using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Contracts.Settings
{
    /// <summary>
    /// Complete set of settings
    /// </summary>
    public class CompleteSettingsModel
    {
        public List<SettingItemModel> Settings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteSettingsModel"/> class.
        /// </summary>
        public CompleteSettingsModel()
        {
            Settings = new List<SettingItemModel>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteSettingsModel"/> class.
        /// </summary>
        /// <param name="settingItem">The setting item.</param>
        public CompleteSettingsModel(SettingItemModel settingItem)
        {
            Settings = new[] { settingItem }.ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteSettingsModel"/> class.
        /// </summary>
        /// <param name="settingItems">The setting items.</param>
        public CompleteSettingsModel(IEnumerable<SettingItemModel> settingItems)
        {
            Settings = settingItems.ToList();
        }

        public CompleteSettingsModel(params SettingItemModel[] settingItems)
        {
            Settings = settingItems.ToList();
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Settings));
        }
    }
}
