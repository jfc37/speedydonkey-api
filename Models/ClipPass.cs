namespace Models
{
    public interface IClipPass : IPass
    {
        int ClipsRemaining { get; set; }
    }

    public class ClipPass : Pass, IClipPass
    {
        public virtual int ClipsRemaining { get; set; }
    }
}