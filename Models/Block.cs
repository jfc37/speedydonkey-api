using System;
using System.Collections.Generic;

namespace Models
{
    public interface IBlock
    {
        IList<ITeacher> Teachers { get; set; }
        ICollection<IUser> EnroledStudents { get; set; }
        ILevel Level { get; set; }
        ICollection<IClass> Classes { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Name { get; set; }
        int Id { get; set; }
    }

    public class Block : IBlock, IEntity, IDatabaseEntity
    {
        public virtual IList<ITeacher> Teachers { get; set; }
        public virtual ICollection<IUser> EnroledStudents { get; set; }
        public virtual ILevel Level { get; set; }
        public virtual ICollection<IClass> Classes { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
    }
}