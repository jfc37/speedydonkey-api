using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface IExamRepository
    {
        IEnumerable<Exam> GetAll(int courseId);
        Exam Get(int examId);

        Exam Create(Exam exam);

        Exam Update(Exam exam);
    }

    public class ExamRepository : IExamRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public ExamRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Exam> GetAll(int courseId)
        {
            return _context.CourseWork.OfType<Exam>()
                .Include(x => x.Course)
                .Where(x => x.Course.Id == courseId)
                .ToList();
        }

        public Exam Get(int examId)
        {
            return _context.CourseWork.OfType<Exam>()
                .Include(x => x.Course)
                .SingleOrDefault(x => x.Id == examId);
        }

        public Exam Create(Exam exam)
        {
            var course = _context.Courses.Single(x => x.Id == exam.Course.Id);
            exam.Course = course;
            course.Exams.Add(exam);

            _context.CourseWork.Add(exam);
            _context.SaveChanges();
            return exam;
        }

        public Exam Update(Exam exam)
        {
            _context.SaveChanges();
            return exam;
        }
    }
}
