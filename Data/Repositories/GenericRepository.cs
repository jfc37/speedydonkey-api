using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Models;
using NHibernate;
using NHibernate.Linq;
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
        IQueryable<TEntity> Queryable();
        
        TEntity Get(int id);
        
        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(int id);

        void SoftDelete(int id);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity , IDatabaseEntity
    {
        private readonly ISession _session;

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _session.Query<TEntity>()
                .Where(x => !x.IsDeleted);
        }

        [Log]
        public TEntity Get(int id)
        {
            var entity = _session.Get<TEntity>(id);

            if (entity == null)
            {
                return null;
            }

            if (entity.IsDeleted)
            {
                return null;
            }

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

        public void SoftDelete(int id)
        {
            var entity = Get(id);
            entity.IsDeleted = true;

            Update(entity);
        }
    }
}