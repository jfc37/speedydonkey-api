using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class CreateStandAloneEvent : ICrudAction<StandAloneEvent>
    {
        public CreateStandAloneEvent(StandAloneEvent standAloneEvent)
        {
            ActionAgainst = standAloneEvent;
        }

        public StandAloneEvent ActionAgainst { get; set; }
    }
}