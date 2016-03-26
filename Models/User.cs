using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;

namespace Models
{
    public class User : DatabaseEntity
    {
        public User()
        {
            Schedule = new List<Event>();
            EnroledBlocks = new List<Block>();
            Passes = new List<Pass>();
        }
        public User(int id) : this()
        {
            Id = id;
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(GlobalId), nameof(Id), nameof(FirstName), nameof(Surname), nameof(Email));
        }

        public virtual string GlobalId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string FullName => $"{FirstName} {Surname}";
        public virtual IList<Event> Schedule { get; set; }
        public virtual ICollection<Block> EnroledBlocks { get; set; }
        public virtual IList<Pass> Passes { get; set; }
        public virtual string Claims { get; set; } 
        public virtual bool DoNotEmail { get; set; }
        public virtual bool AgreesToTerms { get; set; }

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
