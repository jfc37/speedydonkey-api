using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course Get(int courseId);

        Course Create(Course course);

        Course Update(Course course);

        void Delete(Course course);
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CourseRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses
                .Include(x => x.Professors)
                .Include(x => x.Students)
                .Include(x => x.Notices)
                .Include(x => x.Lectures)
                .Include(x => x.Assignments)
                .Include(x => x.Exams)
                .ToList();
        }

        public Course Get(int courseId)
        {
            return _context.Courses
                .Include(x => x.Professors)
                .Include(x => x.Students)
                .Include(x => x.Notices)
                .Include(x => x.Lectures)
                .Include(x => x.Assignments)
                .Include(x => x.Exams)
                .SingleOrDefault(x => x.Id == courseId);
        }

        public Course Create(Course course)
        {
            var createdBy = course.Professors.Single();
            course.Professors.Clear();
            var professor = _context.People.OfType<Professor>().Single(x => x.Id == createdBy.Id);
            course.Professors.Add(professor);

            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public Course Update(Course course)
        {
            _context.SaveChanges();
            return course;
        }

        public void Delete(Course course)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }
}
