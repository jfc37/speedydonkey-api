using System;

namespace Models
{
    public interface IAvailableTime
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
    }
}