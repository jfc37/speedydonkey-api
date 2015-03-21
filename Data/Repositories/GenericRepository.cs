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

        void Delete(TEntity entity);
    }

    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity 
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
            return completedSearch.ToList();
        }

        public TEntity Get(int id)
        {
            return _session.Get<TEntity>(id);
        }

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

        public TEntity Create(TEntity entity)
        {
            _session.Save(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _session.Update(entity, entity.Id);
            _session.Flush();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}