using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class RemoveStudentFromEvent : SystemAction<Event>
    {
        public RemoveStudentFromEvent(Event theEvent)
        {
            ActionAgainst = theEvent;
        }
    }
}