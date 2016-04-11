namespace Contracts.Events
{
    public class EventForRegistrationModel : StandAloneEventModel
    {
        public bool IsAlreadyRegistered { get; set; }
        public int SpacesAvailable { get; set; }
    }
}