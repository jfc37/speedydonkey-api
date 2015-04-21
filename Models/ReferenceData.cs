namespace Models
{
    public interface IReferenceData
    {
        int Id { get; set; }
        string Type { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Value { get; set; }
    }

    public class ReferenceData : IReferenceData, IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public virtual string Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }
    }
}
