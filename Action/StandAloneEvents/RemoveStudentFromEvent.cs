using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class RemoveStudentFromEvent : IAction<Event>
    {
        public Event ActionAgainst { get; set; }

        public RemoveStudentFromEvent(Event theEvent)
        {
            ActionAgainst = theEvent;
        }
    }
}