using Action;
using ActionHandlers.CreateHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdatePassHandler : UpdateEntityHandler<UpdatePass, Pass>
    {
        private readonly IRepository<Pass> _repository;

        public UpdatePassHandler(IRepository<Pass> repository, ICommonInterfaceCloner cloner)
            : base(repository, cloner)
        {
            _repository = repository;
        }

        protected override void PreHandle(ICrudAction<Pass> action)
        {
            var savedPass = _repository.Get(action.ActionAgainst.Id);
            savedPass.PaymentStatus = action.ActionAgainst.PaymentStatus;
            savedPass.StartDate = action.ActionAgainst.StartDate;
            savedPass.EndDate = action.ActionAgainst.EndDate;

            var savedClipPass = savedPass as ClipPass;
            var updatingClipPass = action.ActionAgainst as ClipPass;
            if (savedClipPass != null && updatingClipPass != null)
            {
                savedClipPass.ClipsRemaining = updatingClipPass.ClipsRemaining;
                action.ActionAgainst = savedClipPass;
            }
            else
            {
                action.ActionAgainst = savedPass;
            }
        }
    }
}
