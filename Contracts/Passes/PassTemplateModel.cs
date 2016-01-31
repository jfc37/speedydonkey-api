using Common;
using Common.Extensions;

namespace Contracts.Passes
{
    public class PassTemplateModel : IEntity
    {
        public string Description { get; set; }
        public string PassType { get; set; }
        public decimal Cost { get; set; }
        public int WeeksValidFor { get; set; }
        public int ClassesValidFor { get; set; }
        public bool AvailableForPurchase { get; set; }
        public int Id { get; set; }
        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Description));
        }
    }
}