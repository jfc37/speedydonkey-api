using Actions;
using Models;

namespace Action
{
    public class CreateLevel : ICreateAction<Level>
    {
        public CreateLevel(Level level)
        {
            ActionAgainst = level;
        }

        public Level ActionAgainst { get; set; }
    }
}
