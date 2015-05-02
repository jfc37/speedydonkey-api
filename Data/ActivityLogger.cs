using System;
using Common;
using Data.Repositories;
using Models;
using NHibernate;

namespace Data
{
    public interface IActivityLogger
    {
        void Log(ActivityGroup activityGroup, ActivityType type, string text);
    }
    public class ActivityLogger : IActivityLogger
    {
        private readonly ICurrentUser _currentUser;
        private readonly Guid _activitySession;
        private ISession _dbSession;

        public ActivityLogger(ICurrentUser currentUser, ISession dbSession)
        {
            _currentUser = currentUser;
            _dbSession = dbSession;

            _activitySession = Guid.NewGuid();
        }

        public void Log(ActivityGroup activityGroup, ActivityType type, string text)
        {
            var activityLog = new ActivityLog
            {
                ActivityGroup = activityGroup,
                ActivityText = text,
                ActivityType = type,
                DateTimeStamp = DateTime.Now,
                PerformingUserId = _currentUser.Id,
                Session = _activitySession
            };
            Create(activityLog);
        }

        private void Create(ActivityLog entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            using (var transaction = _dbSession.BeginTransaction())
            {
                _dbSession.Save(entity);
                transaction.Commit();
            }
        }
    }
}
