using Actions;
using Models;

namespace Action
{
    public class CreatePassTemplate : SystemAction<PassTemplate>, ICrudAction<PassTemplate>
    {
        public CreatePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
    }
}
