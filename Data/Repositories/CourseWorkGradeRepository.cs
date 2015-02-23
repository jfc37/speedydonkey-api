using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface ICourseWorkGradeRepository
    {
        IEnumerable<CourseWorkGrade> GetAll(int studentId, int courseId);
        CourseWorkGrade Get(int studentId, int courseId, int courseWorkId);
        CourseWorkGrade Create(CourseWorkGrade courseWorkGrade);
        CourseWorkGrade Update(CourseWorkGrade courseWorkGrade);
        void Delete(CourseWorkGrade courseWorkGrade);
    }

    public class CourseWorkGradeRepository : ICourseWorkGradeRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CourseWorkGradeRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseWorkGrade> GetAll(int studentId, int courseId)
        {
            return _context.CourseWorkGrades
                .Include(x => x.CourseWork)
                .Include(x => x.CourseGrade)
                .Include(x => x.CourseGrade.Student)
                .Include(x => x.CourseGrade.Course)
                .Where(x => x.CourseGrade.Student.Id == studentId && x.CourseGrade.Course.Id == courseId)
                .ToList();
        }

        public CourseWorkGrade Get(int studentId, int courseId, int courseWorkId)
        {
            return _context.CourseWorkGrades
                .Include(x => x.CourseWork)
                .Include(x => x.CourseGrade)
                .Include(x => x.CourseGrade.Student)
                .Include(x => x.CourseGrade.Course)
                .FirstOrDefault(x => x.CourseGrade.Student.Id == studentId && x.CourseGrade.Course.Id == courseId && x.CourseWork.Id == courseWorkId);
        }

        public CourseWorkGrade Create(CourseWorkGrade courseWorkGrade)
        {
            courseWorkGrade.CourseWork = _context.CourseWork.Single(x => x.Id == courseWorkGrade.CourseWork.Id);

            var courseGrade = _context.CourseGrades.Single(x => x.Course.Id == courseWorkGrade.CourseGrade.Course.Id && x.Student.Id == courseWorkGrade.CourseGrade.Student.Id);
            courseWorkGrade.CourseGrade = courseGrade;
            courseGrade.CourseWorkGrades.Add(courseWorkGrade);

            _context.CourseWorkGrades.Add(courseWorkGrade);
            _context.SaveChanges();
            return courseWorkGrade;
        }

        public CourseWorkGrade Update(CourseWorkGrade courseWorkGrade)
        {
            _context.SaveChanges();
            return courseWorkGrade;
        }

        public void Delete(CourseWorkGrade courseWorkGrade)
        {
            _context.CourseWorkGrades.Remove(courseWorkGrade);
            _context.SaveChanges();
        }
    }
}
