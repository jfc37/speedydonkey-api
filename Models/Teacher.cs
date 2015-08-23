using System;
using System.Collections.Generic;
using Common;

namespace Models
{
    public interface ITeacher : IEntity
    {
        IUser User { get; set; }
        ICollection<IClass> Classes { get; set; } 
    }

    public class Teacher : ITeacher, IDatabaseEntity
    {
        public virtual IUser User { get; set; }
        public virtual ICollection<IClass> Classes { get; set; }
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
    }
}
