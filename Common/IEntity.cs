using System;

namespace Common
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedDateTime { get; set; }
        DateTime? LastUpdatedDateTime { get; set; }
    }
}