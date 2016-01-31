using Actions;
using Models;

namespace Action
{
    public class UpdatePassTemplate : SystemAction<PassTemplate>, ICrudAction<PassTemplate>
    {
        public UpdatePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
    }
}
