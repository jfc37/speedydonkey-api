using System;

namespace Models
{
    public class ActivityLog : IEntity, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public virtual Guid Session { get; set; }
        public virtual int PerformingUserId { get; set; }
        public virtual DateTime DateTimeStamp { get; set; }
        public virtual ActivityGroup ActivityGroup { get; set; }
        public virtual ActivityType ActivityType { get; set; }
        public virtual string ActivityText { get; set; }
    }

    public enum ActivityGroup
    {
        DatabaseAccess,
        PerformAction,
        Error
    }

    public enum ActivityType
    {
        GetAll,
        GetAllWithChildren,
        GetById,
        GetByIdWithChildren,
        Create,
        Update,
        Delete,
        Successful,
        Beginning,
        FailedValidation,
        Unhandled
    }
}
