using System;
using Models;

namespace Actions
{
    public class CreateRegistration : SystemAction<Registration>, ICrudAction<Registration>
    {
        public CreateRegistration(Registration registration)
        {
            ActionAgainst = registration;
        }
    }
}
