using Actions;

namespace ActionHandlers
{
    /// <summary>
    /// The handler of an action, with it's returning result
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IActionHandlerWithResult<in TAction, out TObject, out TResult> where TAction : SystemAction<TObject>
    {
        TResult Handle(TAction action);
    }
}