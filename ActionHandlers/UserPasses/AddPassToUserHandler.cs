using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.UserPasses
{
    public class AddPassToUserHandler : IActionHandler<AddPassToUser, User>
    {
        private readonly IUserPassAppender _userPassAppender;
        private readonly IRepository<User> _userRepository;

        public AddPassToUserHandler(
            IUserPassAppender userPassAppender,
            IRepository<User> userRepository)
        {
            _userPassAppender = userPassAppender;
            _userRepository = userRepository;
        }

        public User Handle(AddPassToUser action)
        {
            var user = _userRepository.Get(action.ActionAgainst.Id);
            foreach (var pass in action.ActionAgainst.Passes)
            {
                _userPassAppender.AddPassToUser(user, pass);
            }
            return _userRepository.Update(user);
        }
    }
}
