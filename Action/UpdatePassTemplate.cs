using System;
using Actions;
using Models;

namespace Action
{
    public class UpdatePassTemplate : ICrudAction<PassTemplate>
    {
        public UpdatePassTemplate(PassTemplate passTemplate)
        {
            ActionAgainst = passTemplate;
        }
        public PassTemplate ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Update pass template {0}", ActionAgainst.Description);
            }
        }
    }
}
