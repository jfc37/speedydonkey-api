using Actions;
using Models;

namespace Action
{
    public class CreatePassTemplate : ICrudAction<PassTemplate>
    {
        public CreatePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
        public PassTemplate ActionAgainst { get; set; }
    }
}
