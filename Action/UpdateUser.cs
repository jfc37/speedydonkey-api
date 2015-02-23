using Models;

namespace Actions
{
    public class UpdateUser : IAction<User>
    {
        public UpdateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
