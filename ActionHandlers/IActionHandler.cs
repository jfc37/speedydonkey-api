using Actions;

namespace ActionHandlers
{
    /// <summary>
    /// The handler of an action, with it's returning result
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IActionHandler<in TAction, TResult> where TAction : IAction<TResult>
    {
        TResult Handle(TAction action);
    }
}
