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
        public IAccount Account { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public ISchedule Schedule { get; set; }
        public IList<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public int Id { get; set; }
    }
}
