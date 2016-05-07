namespace Contracts.Blocks
{
    public class BlockForRegistrationModel : BlockModel
    {
        public bool IsAlreadyRegistered { get; set; }
        public int SpacesAvailable { get; set; }
    }
}