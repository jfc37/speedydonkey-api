using System.Linq;
using Common.Extensions;
using Contracts.Settings;
using Models.Settings;

namespace Contracts.MappingExtensions
{
    public static class SettingMappingExtensions
    {
        public static CompleteSettings ToEntity(this CompleteSettingsModel instance)
        {
            if (instance.IsNull())
            {
                return null;
            }

            return new CompleteSettings(instance.Settings.Select(x => x.ToEntity()));
        }
        public static SettingItem ToEntity(this SettingItemModel instance)
        {
            return instance.IsNull() 
                ? null 
                : new SettingItem(instance.Name.Parse<SettingTypes>(), instance.Value);
        }

        public static CompleteSettingsModel ToModel(this CompleteSettings instance)
        {
            if (instance.IsNull())
            {
                return null;
            }

            return new CompleteSettingsModel(instance.Settings.Select(x => x.ToModel()));
        }
        public static SettingItemModel ToModel(this SettingItem instance)
        {
            return instance.IsNull() 
                ? null 
                : new SettingItemModel(instance.Name.ToString(), instance.Value);
        }
    }
}
