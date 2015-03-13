using System.Collections.Generic;

namespace Models
{
    public interface ITeacher : IUser
    {
        IList<IAvailableTime> AvailableTimes { get; set; }
    }
}