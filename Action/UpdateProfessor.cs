using Models;

namespace Actions
{
    public class UpdateProfessor : IAction<Professor>
    {
        public UpdateProfessor(Professor professor)
        {
            ActionAgainst = professor;
        }

        public Professor ActionAgainst { get; set; }
    }
}
