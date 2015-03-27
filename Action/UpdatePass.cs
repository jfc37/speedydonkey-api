using Actions;
using Models;

namespace Action
{
    public class UpdatePass : ICreateAction<Pass>
    {
        public UpdatePass(Pass pass)
        {
            ActionAgainst = pass;
        }

        public Pass ActionAgainst { get; set; }
    }
}
