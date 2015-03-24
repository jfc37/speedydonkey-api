using System;
using System.Collections.Generic;

namespace Models
{
    public interface IUser
    {
        int Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        string FullName { get; }
        IList<IBooking> Schedule { get; set; } 
        ICollection<IBlock> EnroledBlocks { get; set; }
        IList<IPass> Passes { get; set; }  
    }

    public class User : IUser, IEntity
    {
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public virtual IList<IBooking> Schedule { get; set; }
        public virtual ICollection<IBlock> EnroledBlocks { get; set; }
        public virtual IList<IPass> Passes { get; set; }
        public virtual int Id { get; set; }
    }
}
