using System;

namespace Models
{
    public interface IDatabaseEntity
    {
        DateTime CreatedDateTime { get; set; }
        DateTime? LastUpdatedDateTime { get; set; }
    }
}