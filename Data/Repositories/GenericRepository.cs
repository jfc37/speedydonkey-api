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
            //User userAlias = null;
            //Booking bookingAlias = null;
            //Event eventAlias = null;


            //var events = _session.QueryOver<Event>(() => eventAlias)
            //    .Future<Event>();

            //var bookings = _session.QueryOver<Booking>(() => bookingAlias)
            //    .JoinAlias(x => x.Event, () => eventAlias)
            //    .Future<Booking>();

            //var users = _session.QueryOver<User>(() => userAlias)
            //    .JoinAlias(x => x.Schedule, () => bookingAlias)
            //    .Where(x => x.Id == id)
            //    .Future<User>();

            //var userList = users.ToList();


            var search = _session.CreateCriteria<User>()
                .SetFetchMode("Schedule", FetchMode.Join)
                .SetFetchMode("Schedule.Event", FetchMode.Join)
                .Future<User>();
            var user = search.First(x => x.Id == id);
            var timeBefore = DateTime.Now.AddHours(-1);
            return user.Schedule.Select(x => x.Event)
                .Where(x => x.StartTime > timeBefore)
                .OrderBy(x => x.StartTime)
                .Take(10).ToList();
        }
    }

    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);

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

        public TEntity Get(int id)
        {
            return _session.Get<TEntity>(id);
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