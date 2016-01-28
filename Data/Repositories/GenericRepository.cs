using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Models;
using NHibernate;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace Data.Repositories
{
    public interface IAdvancedRepository<TEntity, out TModel> where TEntity : IEntity
    {
        TModel Get(int id);
    }

    public class UserScheduleRepository : IAdvancedRepository<User, IList<Event>>
    {
        private readonly ISession _session;

        public UserScheduleRepository(ISession session)
        {
            _session = session;
        }

        public IList<Event> Get(int id)
        {
            var timeBefore = DateTime.Now.AddHours(-1);
            var search = _session.CreateCriteria<User>()
                .SetFetchMode("Schedule", FetchMode.Join)
                .SetFetchMode("Schedule.Event", FetchMode.Join)
                .Future<User>();
            var user = search.First(x => x.Id == id);
            var schedule = user.Schedule
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
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity , IDatabaseEntity
    {
        private readonly ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _session.CreateCriteria<TEntity>()
                .List<TEntity>();
        }

        public IEnumerable<TEntity> GetAllWithChildren(IList<string> children)
        {
            var search = _session.CreateCriteria<TEntity>();
            foreach (var child in children)
            {
                search.SetFetchMode(child, FetchMode.Join);
            }
            var completedSearch = search.Future<TEntity>();

            var groupedById = completedSearch.GroupBy(x => x.Id);
            var justTheFirstOfEach = groupedById.Select(x => x.First());
            return justTheFirstOfEach;
        }

        [Log]
        public TEntity Get(int id)
        {
            var entity = _session.Get<TEntity>(id);
            return entity;
        }

        [Log]
        public TEntity GetWithChildren(int id, IList<string> children)
        {
            var search = _session.CreateCriteria<TEntity>();
            foreach (var child in children)
            {
                search.SetFetchMode(child, FetchMode.Select);
            }
            var completedSearch = search.Future<TEntity>();

            var entity = completedSearch.First(x => x.Id == id);
            return entity;
        }

        [Log]
        public TEntity Create(TEntity entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(entity);
                transaction.Commit();
            }
            return entity;
        }

        [Log]
        public TEntity Update(TEntity entity)
        {
            entity.LastUpdatedDateTime = DateTime.Now;
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity, entity.Id);
                _session.Flush();
                transaction.Commit();
            }
            return entity;
        }

        [Log]
        public void Delete(int id)
        {
            var entity = Get(id);
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                _session.Flush();
                transaction.Commit();
            }
        }
    }
}