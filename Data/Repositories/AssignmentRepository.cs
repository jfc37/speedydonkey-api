using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface IAssignmentRepository
    {
        IEnumerable<Assignment> GetAll(int courseId);
        Assignment Get(int assignmentId);

        Assignment Create(Assignment assignment);

        Assignment Update(Assignment assignment);
    }

    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public AssignmentRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Assignment> GetAll(int courseId)
        {
            return _context.CourseWork.OfType<Assignment>()
                .Include(x => x.Course)
                .Where(x => x.Course.Id == courseId)
                .ToList();
        }

        public Assignment Get(int assignmentId)
        {
            return _context.CourseWork.OfType<Assignment>()
                .Include(x => x.Course)
                .SingleOrDefault(x => x.Id == assignmentId);
        }

        public Assignment Create(Assignment assignment)
        {
            var course = _context.Courses.Single(x => x.Id == assignment.Course.Id);
            assignment.Course = course;
            course.Assignments.Add(assignment);

            _context.CourseWork.Add(assignment);
            _context.SaveChanges();
            return assignment;
        }

        public Assignment Update(Assignment assignment)
        {
            _context.SaveChanges();
            return assignment;
        }
    }
}
