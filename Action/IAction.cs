namespace Actions
{
    /// <summary>
    /// An action to be performed against TObject
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IAction<TObject>
    {
        TObject ActionAgainst { get; set; }
    }
}
