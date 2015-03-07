using System.Collections.Generic;

namespace Models
{
    public interface IUser
    {
        int Id { get; set; }
        IAccount Account { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        ISchedule Schedule { get; set; } 
        IList<IBlock> EnroledBlocks { get; set; }
        IList<IPass> Passes { get; set; }  
    }

    public class User : IUser, IEntity
    {
        public virtual IAccount Account { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual ISchedule Schedule { get; set; }
        public virtual IList<IBlock> EnroledBlocks { get; set; }
        public virtual IList<IPass> Passes { get; set; }
        public virtual int Id { get; set; }
    }
}
