using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ITeacher : IUser, IEntity, IDatabaseEntity
    {
        IList<IClass> Classes { get; set; } 
    }

    public class Teacher : User, ITeacher
    {
        public virtual IList<IClass> Classes { get; set; }
    }
}
