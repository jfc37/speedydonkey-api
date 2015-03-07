using System;
using System.Collections.Generic;

namespace Models
{
    public interface ILevel
    {
        string Name { get; set; }
        IRoom Room { get; set; }
        IList<ITeacher> Teachers { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        int ClassesInBlock { get; set; }
    }
}