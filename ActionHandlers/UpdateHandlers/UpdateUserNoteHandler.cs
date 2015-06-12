using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateUserNoteHandler : IActionHandler<UpdateUserNote, User>
    {
        private readonly IRepository<User> _repository;

        public UpdateUserNoteHandler(
            IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Handle(UpdateUserNote action)
        {
            var user = _repository.Get(action.ActionAgainst.Id);
            user.Note = action.ActionAgainst.Note;

            _repository.Update(user);
            return user;
        }
    }
}
