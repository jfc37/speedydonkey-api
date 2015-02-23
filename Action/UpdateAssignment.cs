using Models;

namespace Actions
{
    public class UpdateAssignment : IAction<Assignment>
    {
        public UpdateAssignment(Assignment assignment)
        {
            ActionAgainst = assignment;
        }

        public Assignment ActionAgainst { get; set; }
    }
}
