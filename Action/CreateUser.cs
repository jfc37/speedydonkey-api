using Models;

namespace Actions
{
    public class CreateUser : ICrudAction<User>
    {
        public CreateUser(User user)
        {
            ActionAgainst = user;
        }

        public User ActionAgainst { get; set; }
    }
}
