using System;

namespace Models
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedDateTime { get; set; }
        DateTime? LastUpdatedDateTime { get; set; }
    }

    public interface IDatabaseEntity
    {
        bool Deleted { get; set; }
    }
}