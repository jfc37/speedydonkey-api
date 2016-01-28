using Common.Extensions;

namespace Actions
{
    /// <summary>
    /// An action to be performed against TObject
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IAction<TObject>
    {
        /// <summary>
        /// Gets or sets the action against.
        /// </summary>
        /// <value>
        /// The action against.
        /// </value>
        TObject ActionAgainst { get; set; }
    }

    /// <summary>
    /// An action to be performed against TObject
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public abstract class SystemAction<TObject> : IAction<TObject>
    {
        /// <summary>
        /// Gets or sets the action against.
        /// </summary>
        /// <value>
        /// The action against.
        /// </value>
        public TObject ActionAgainst { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(ActionAgainst));
        }
    }
}
