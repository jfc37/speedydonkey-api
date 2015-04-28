using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using NHibernate;

namespace Data.Repositories
{
    public interface IAdvancedRepository<TEntity, TModel> where TEntity : IEntity
    {
        TModel Get(int id);
    }

    public class UserScheduleRepository : IAdvancedRepository<User, IList<IEvent>>
    {
        private readonly ISession _session;

        public UserScheduleRepository(ISession session)
        {
            _session = session;
        }

        public IList<IEvent> Get(int id)
        {
            var timeBefore = DateTime.Now.AddHours(-1);
            var search = _session.CreateCriteria<User>()
                .SetFetchMode("Schedule", FetchMode.Join)
                .SetFetchMode("Schedule.Event", FetchMode.Join)
                .Future<User>();
            var user = search.First(x => x.Id == id);
            var schedule = user.Schedule.Select(x => x.Event)
                .Where(x => x.StartTime > timeBefore)
                .OrderBy(x => x.StartTime)
                .Take(10).ToList();
            return schedule;
        }
    }

    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAllWithChildren(IList<string> children);

        TEntity Get(int id);

        TEntity GetWithChildren(int id, IList<string> children);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(int id);

        void PermanentDelete(int id);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity , IDatabaseEntity
    {
        private readonly ISession _session;
        private readonly IActivityLogger _activityLogger;

        public GenericRepository(ISession session, IActivityLogger activityLogger)
        {
            _session = session;
            _activityLogger = activityLogger;
        }

        public IEnumerable<TEntity> GetAll()
        {
            Log(ActivityType.GetAll);
            return _session.CreateCriteria<TEntity>()
                .List<TEntity>()
                .Where(x => !x.Deleted);
        }

        public IEnumerable<TEntity> GetAllWithChildren(IList<string> children)
        {
            var search = _session.CreateCriteria<TEntity>();
            foreach (var child in children)
            {
                search.SetFetchMode(child, FetchMode.Join);
            }
            var completedSearch = search.Future<TEntity>()
                .Where(x => !x.Deleted);

            var groupedById = completedSearch.GroupBy(x => x.Id);
            var justTheFirstOfEach = groupedById.Select(x => x.First());
            Log(ActivityType.GetAllWithChildren, String.Format("Children: {0}", String.Join(", ", children)));
            return justTheFirstOfEach;
        }

        public TEntity Get(int id)
        {
            Log(ActivityType.GetById, String.Format("Id: {0}", id));
            var entity = _session.Get<TEntity>(id);
            return entity == null || entity.Deleted ? null : entity;
        }

        public TEntity GetWithChildren(int id, IList<string> children)
        {
            var search = _session.CreateCriteria<TEntity>();
            foreach (var child in children)
            {
                search.SetFetchMode(child, FetchMode.Select);
            }
            var completedSearch = search.Future<TEntity>()
                .Where(x => !x.Deleted);

            var entity = completedSearch.First(x => x.Id == id);
            Log(ActivityType.GetByIdWithChildren, String.Format("Id: {0}, Children: {1}", id, String.Join(", ", children)));
            return entity;
        }

        public TEntity Create(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(entity);
                transaction.Commit();
            }
            Log(ActivityType.Create, String.Format("Id: {0}", entity.Id));
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity, entity.Id);
                _session.Flush();
                transaction.Commit();
            }
            Log(ActivityType.Update, String.Format("Id: {0}", entity.Id));
            return entity;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            Update(entity);
            Log(ActivityType.Delete, String.Format("Id: {0}", entity.Id));
        }

        public void PermanentDelete(int id)
        {
            var entity = Get(id);
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                _session.Flush();
                transaction.Commit();
            }
            Log(ActivityType.PermanentDelete, String.Format("Id: {0}", entity.Id));
        }

        private void Log(ActivityType activityType, string extraDetails = "")
        {
            var activityText = String.Format("Entity: {0}", typeof (TEntity).Name);
            if (!String.IsNullOrWhiteSpace(extraDetails))
                activityText = activityText + ", " + extraDetails;

            _activityLogger.Log(ActivityGroup.DatabaseAccess, activityType,
                activityText);
        }
    }
}