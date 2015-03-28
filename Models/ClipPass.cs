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
            ClipsRemaining = ClipsRemaining - 1;
        }

        public override void RefundForClass()
        {
            ClipsRemaining = ClipsRemaining + 1;
        }
    }
}