using System;
using Models;

namespace Actions
{
    public class CreateRegistration : ICrudAction<Registration>
    {
        public CreateRegistration(Registration registration)
        {
            ActionAgainst = registration;
        }

        public Registration ActionAgainst { get; set; }
        public string LogText
        {
            get
            {
                return String.Format("Create registration");
            }
        }
    }
}
