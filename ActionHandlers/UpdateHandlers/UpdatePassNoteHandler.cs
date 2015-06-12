using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdatePassNoteHandler : IActionHandler<UpdatePassNote, Pass>
    {
        private readonly IRepository<Pass> _repository;

        public UpdatePassNoteHandler(
            IRepository<Pass> repository)
        {
            _repository = repository;
        }

        public Pass Handle(UpdatePassNote action)
        {
            var pass = _repository.Get(action.ActionAgainst.Id);
            pass.Note = action.ActionAgainst.Note;

            _repository.Update(pass);
            return pass;
        }
    }
}
