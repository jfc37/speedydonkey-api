using Common.Extensions;

namespace Models.Settings
{
    /// <summary>
    /// A single setting item
    /// </summary>
    public class SettingItem : DatabaseEntity
    {
        public SettingItem()
        {

        }
        
        public SettingItem(SettingTypes name, string value)
        {
            Name = name;
            Value = value;
        }

        public virtual SettingTypes Name { get; set; }
        public virtual string Value { get; set; }
        
        public override string ToString()
        {
            return this.ToDebugString(nameof(Name), nameof(Value));
        }
    }
}