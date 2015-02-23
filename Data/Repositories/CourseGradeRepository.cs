using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface ICourseGradeRepository
    {
        IEnumerable<CourseGrade> GetAll(int studentId);
        CourseGrade Get(int studentId, int courseId);
    }

    public class CourseGradeRepository : ICourseGradeRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CourseGradeRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseGrade> GetAll(int studentId)
        {
            return _context.CourseGrades
                .Include(x => x.Student)
                .Include(x => x.CourseWorkGrades)
                .Include(x => x.CourseWorkGrades.Select(y => y.CourseWork))
                .Where(x => x.Student.Id == studentId)
                .ToList();
        }

        public CourseGrade Get(int studentId, int courseId)
        {
            return _context.CourseGrades
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Include(x => x.CourseWorkGrades)
                .Include(x => x.CourseWorkGrades.Select(y => y.CourseWork))
                .FirstOrDefault(x => x.Student.Id == studentId && x.Course.Id == courseId);
        }
    }
}
