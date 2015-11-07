using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Models
{
    public class User : IEntity, IDatabaseEntity
    {
        public User()
        {
            
        }
        public User(int id)
        {
            Id = id;
        }

        public virtual string GlobalId { get; set; }
        public virtual UserStatus Status { get; set; }
        public virtual Guid ActivationKey { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public virtual IList<Event> Schedule { get; set; }
        public virtual ICollection<Block> EnroledBlocks { get; set; }
        public virtual IList<Pass> Passes { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual string Claims { get; set; } 

        public virtual Pass GetPassToUse()
        {
            return Passes.Where(x => x.IsValid())
                .OrderBy(x => x.EndDate)
                .FirstOrDefault();
        }

        public virtual Pass GetPassToRefund()
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

        public virtual string Note { get; set; }
    }

    public enum UserStatus
    {
        Unactiviated,
        Active
    }
}
