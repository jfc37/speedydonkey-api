using System.Collections.Generic;

namespace Models
{
    public interface IClass : IEvent
    {
        IList<IUser> ActualStudents { get; set; }
        IBlock Block { get; set; }

        //has a set of notices
    }
}