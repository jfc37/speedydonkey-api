using System;
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
        public string LogText
        {
            get
            {
                return String.Format("Create pass template {0}", ActionAgainst.Description);
            }
        }
    }
}
