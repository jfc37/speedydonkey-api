using Models;

namespace Actions
{
    public class CreateAssignment : IAction<Assignment>
    {
        public CreateAssignment(Assignment assignment)
        {
            ActionAgainst = assignment;
        }

        public Assignment ActionAgainst { get; set; }
    }
}
