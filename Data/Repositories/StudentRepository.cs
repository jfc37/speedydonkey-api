using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public class StudentRepository : PersonRepository<Student>
    {
        public StudentRepository(ISpeedyDonkeyDbContext context)
            : base(context)
        {
        }

        public override IEnumerable<Student> GetAll()
        {
            return Context.People.OfType<Student>()
                .Include(x => x.EnroledCourses)
                .Include(x => x.CourseGrades)
                .ToList();
        }

        public override Student Get(int personId)
        {
            return Context.People.OfType<Student>()
                .Include(x => x.EnroledCourses)
                .Include(x => x.CourseGrades)
                .ToList()
                .FirstOrDefault(x => x.Id == personId);
        }
    }
}