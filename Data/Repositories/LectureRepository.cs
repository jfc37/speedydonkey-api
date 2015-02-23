using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface ILectureRepository
    {
        IEnumerable<Lecture> GetAll(int courseId);
        Lecture Get(int lectureId);

        Lecture Create(Lecture lecture);

        Lecture Update(Lecture lecture);

        void Delete(Lecture lecture);
    }

    public class LectureRepository : ILectureRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public LectureRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lecture> GetAll(int courseId)
        {
            return _context.Lectures
                .Include(x => x.Course)
                .Where(x => x.Course.Id == courseId)
                .ToList();
        }

        public Lecture Get(int lectureId)
        {
            return _context.Lectures
                .Include(x => x.Course)
                .SingleOrDefault(x => x.Id == lectureId);
        }

        public Lecture Create(Lecture lecture)
        {
            var course = _context.Courses.Single(x => x.Id == lecture.Course.Id);
            lecture.Course = course;
            course.Lectures.Add(lecture);

            _context.Lectures.Add(lecture);
            _context.SaveChanges();
            return lecture;
        }

        public Lecture Update(Lecture lecture)
        {
            _context.SaveChanges();
            return lecture;
        }

        public void Delete(Lecture lecture)
        {
            _context.Lectures.Remove(lecture);
            _context.SaveChanges();
        }
    }
}
