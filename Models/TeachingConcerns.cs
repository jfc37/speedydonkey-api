namespace Models
{
    public interface ITeachingConcerns : IEntity
    {
    }

    public class TeachingConcerns : ITeachingConcerns, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
    }
}
