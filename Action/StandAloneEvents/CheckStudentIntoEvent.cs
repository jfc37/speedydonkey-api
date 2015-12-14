using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class CheckStudentIntoEvent : IAction<Event>
    {
        public Event ActionAgainst { get; set; }

        public CheckStudentIntoEvent(Event theEvent)
        {
            ActionAgainst = theEvent;
        }
    }
}