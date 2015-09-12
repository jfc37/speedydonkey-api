using ActionHandlers;
using Actions;
using Common;

namespace SpeedyDonkeyApi.Controllers
{
    public abstract class EntityPropertyApiController : BaseApiController
    {
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        protected EntityPropertyApiController(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        protected ActionReponse<TActionEntity> PerformAction<TAction, TActionEntity>(TAction action)
            where TAction : IAction<TActionEntity>
            where TActionEntity : class, IEntity
        {
            ActionReponse<TActionEntity> result = _actionHandlerOverlord.HandleAction<TAction, TActionEntity>(action);
            return result;
        }
    }
}