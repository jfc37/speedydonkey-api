using Common.Extensions;

namespace Contracts.Settings
{
    /// <summary>
    /// A single setting item
    /// </summary>
    public class SettingItemModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingItemModel"/> class.
        /// </summary>
        public SettingItemModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingItemModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public SettingItemModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Name), nameof(Value));
        }
    }
}
