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
}
