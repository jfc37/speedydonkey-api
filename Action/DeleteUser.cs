using Models;

namespace Actions
{
    public class DeleteUser : IAction<User>
    {
        public DeleteUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
