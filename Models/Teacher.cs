using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Models
{
    public interface ITeacher : IUser
    {
        ICollection<IClass> Classes { get; set; } 
    }

    public class Teacher : User, ITeacher
    {
        public virtual ICollection<IClass> Classes { get; set; }
    }
}
