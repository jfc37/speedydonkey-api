namespace Models
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IDatabaseEntity
    {
        bool Deleted { get; set; }
    }
}