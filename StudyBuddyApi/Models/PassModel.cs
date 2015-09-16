using System;

namespace SpeedyDonkeyApi.Models
{
    public class PassModel
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PassType { get; set; }

        public string PassNumber
        {
            get
            {
                return String.Format("{0}{1}", CreatedDateTime.ToString("yy"), Id.ToString("D4"));
            }
        }

        public string PaymentStatus { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public UserModel Owner { get; set; }
        public PassStatisticModel PassStatistic { get; set; }

        public bool Valid
        {
            get { return IsValid(); }
        }

        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate <= today && today <= EndDate;
        }

        public string Note { get; set; }
    }

    public class ClipPassModel : PassModel
    {
        public int ClipsRemaining { get; set; }
        public override bool IsValid()
        {
            return ClipsRemaining > 0 && base.IsValid();
        }
    }
}