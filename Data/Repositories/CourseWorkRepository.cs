using System.Collections.Generic;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface ICourseWorkRepository<TCourseWork> where TCourseWork : CourseWork
    {
        IEnumerable<TCourseWork> GetAll();
        TCourseWork Get(int courseWorkId);

        void Delete(TCourseWork courseWork);
    }

    public class CourseWorkRepository : ICourseWorkRepository<CourseWork>
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public CourseWorkRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseWork> GetAll()
        {
            return _context.CourseWork
                .ToList();
        }

        public CourseWork Get(int courseWorkId)
        {
            return _context.CourseWork.OfType<CourseWork>()
                .SingleOrDefault(x => x.Id == courseWorkId);
        }

        public void Delete(CourseWork courseWork)
        {
            _context.CourseWork.Remove(courseWork);
            _context.SaveChanges();
        }
    }
}