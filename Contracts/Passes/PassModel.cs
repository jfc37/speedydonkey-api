using System;
using Common.Extensions;
using Contracts.Users;

namespace Contracts.Passes
{
    public class PassModel
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string PassType { get; set; }

        public string PassNumber => $"{CreatedDateTime.ToString("yy")}{Id.ToString("D4")}";

        public string PaymentStatus { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public UserModel Owner { get; set; }
        public PassStatisticModel PassStatistic { get; set; }

        public bool Valid => IsValid();

        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate <= today && today <= EndDate;
        }

        public string Note { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Description));
        }
    }
}