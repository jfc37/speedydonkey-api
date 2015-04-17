using System;
using Actions;
using Models;

namespace Action
{
    public class DeletePassTemplate : ICrudAction<PassTemplate>
    {
        public DeletePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
        public PassTemplate ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Delete pass template {0}", ActionAgainst.Description);
            }
        }
    }
}
