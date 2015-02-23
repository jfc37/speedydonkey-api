using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface INoticeRepository
    {
        IEnumerable<Notice> GetAll(int courseId);
        Notice Get(int noticeId);

        Notice Create(Notice notice);
        Notice Update(Notice notice);

        void Delete(Notice notice);
    }

    public class NoticeRepository : INoticeRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public NoticeRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notice> GetAll(int courseId)
        {
            return _context.Notices
                .Include(x => x.Course)
                .Where(x => x.Course.Id == courseId)
                .ToList();
        }

        public Notice Get(int noticeId)
        {
            return _context.Notices
                .Include(x => x.Course)
                .SingleOrDefault(x => x.Id == noticeId);
        }

        public Notice Create(Notice notice)
        {
            var course = _context.Courses.Single(x => x.Id == notice.Course.Id);
            notice.Course = course;
            course.Notices.Add(notice);

            _context.Notices.Add(notice);
            _context.SaveChanges();
            return notice;
        }

        public Notice Update(Notice notice)
        {
            _context.SaveChanges();
            return notice;
        }

        public void Delete(Notice notice)
        {
            _context.Notices.Remove(notice);
            _context.SaveChanges();
        }
    }
}
