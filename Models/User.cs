using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public interface IUser : IEntity
    {
        string Email { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string Surname { get; set; }
        IList<IBooking> Schedule { get; set; } 
        ICollection<IBlock> EnroledBlocks { get; set; }
        IList<IPass> Passes { get; set; }
    }

    public class User : IUser, IDatabaseEntity
    {

        public virtual UserStatus Status { get; set; }
        public virtual Guid ActivationKey { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public virtual IList<IBooking> Schedule { get; set; }
        public virtual ICollection<IBlock> EnroledBlocks { get; set; }
        public virtual IList<IPass> Passes { get; set; }
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public virtual string Claims { get; set; } 

        public virtual IPass GetPassToUse()
        {
            return Passes.Where(x => x.IsValid())
                .OrderBy(x => x.EndDate)
                .FirstOrDefault();
        }

        public virtual IPass GetPassToRefund()
        {
            if (Passes.Any(x => x.IsValid()))
            {
                return Passes.Where(x => x.IsValid())
                    .OrderBy(x => x.EndDate)
                    .FirstOrDefault();
            }
            else
            {
                return Passes.OrderByDescending(x => x.EndDate)
                    .FirstOrDefault();
            }
        }
    }

    public enum UserStatus
    {
        Unactiviated,
        Active
    }
}
