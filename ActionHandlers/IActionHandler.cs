using Actions;

namespace ActionHandlers
{
    /// <summary>
    /// The handler of an action, with it's returning result
    /// </summary>
    /// <typeparam name="TAction"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    public interface IActionHandler<in TAction, out TObject> where TAction : IAction<TObject>
    {
        /// <summary>
        /// Handles the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        TObject Handle(TAction action);
    }
}
