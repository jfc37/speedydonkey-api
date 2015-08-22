using System;
using Common;

namespace Models
{
    public interface IActivityLog : IEntity
    {
        Guid Session { get; set; }
        int PerformingUserId { get; set; }
        DateTime DateTimeStamp { get; set; }
        ActivityGroup ActivityGroup { get; set; }
        ActivityType ActivityType { get; set; }
        string ActivityText { get; set; }
    }

    public class ActivityLog : IDatabaseEntity, IActivityLog
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
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
        PermanentDelete,
        Successful,
        Beginning,
        FailedValidation,
        Unhandled,
        Payment
    }
}
