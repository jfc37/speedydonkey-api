using System;
using System.Collections.Generic;

namespace Models
{
    public interface IBlock
    {
        ICollection<IUser> EnroledStudents { get; set; }
        ILevel Level { get; set; }
        IList<IClass> Classes { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string Name { get; set; }
        int Id { get; set; }
    }

    public class Block : IBlock, IEntity
    {
        public virtual ICollection<IUser> EnroledStudents { get; set; }
        public virtual ILevel Level { get; set; }
        public virtual IList<IClass> Classes { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
    }
}