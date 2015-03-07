namespace Models
{
    public interface IClipPass : IPass
    {
        int ClipsRemaining { get; set; }
    }
}