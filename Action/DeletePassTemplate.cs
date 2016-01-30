using System;
using Actions;
using Models;

namespace Action
{
    public class DeletePassTemplate : SystemAction<PassTemplate>, ICrudAction<PassTemplate>
    {
        public DeletePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
    }
}
