using System.Collections.Generic;
using Models;
using NHibernate;

namespace Data.Repositories
{
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
            //using (var session = GetSession())
            //{
            //    //using (var transaction = session.BeginTransaction())
            //    //{
            //    //    session.Update(entity, entity.Id);
            //    //    session.Flush();
            //    //    transaction.Commit();
            //    //}
            //}
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