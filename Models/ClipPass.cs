namespace Models
{
    public interface IClipPass : IPass
    {
        int ClipsRemaining { get; set; }
    }

    public class ClipPass : Pass, IClipPass
    {
        public virtual int ClipsRemaining { get; set; }

        public override bool IsValid()
        {
            return ClipsRemaining > 0 && base.IsValid();
        }

        public override void PayForClass()
        {
            base.PayForClass();
            ClipsRemaining = ClipsRemaining - 1;
            PassStatistic.CostPerClass = Cost/(ClipsRemaining + PassStatistic.NumberOfClassesAttended);
        }

        public override void RefundForClass()
        {
            base.RefundForClass();
            ClipsRemaining = ClipsRemaining + 1;
            PassStatistic.CostPerClass = Cost / (ClipsRemaining + PassStatistic.NumberOfClassesAttended);
        }
    }
}