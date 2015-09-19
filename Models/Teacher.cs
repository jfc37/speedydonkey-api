using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public class Teacher : IEntity, IDatabaseEntity
    {
        public virtual User User { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }

        public Teacher()
        {
            
        }

        public Teacher(User user)
        {
            User = user;
        }

        public Teacher(int id)
        {
            Id = id;
        }
    }
}
