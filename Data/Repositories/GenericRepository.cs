using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Models;
using NHibernate;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;

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

    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity 
    {
        private ISession GetSession()
        {
            return CreateSessionFactory().OpenSession();
        }


        public IEnumerable<TEntity> GetAll()
        {
            using (var session = GetSession())
            {
                return session.CreateCriteria<TEntity>()
                    .List<TEntity>();
            }
        }

        public TEntity Get(int id)
        {
            using (var session = GetSession())
            {
                return session.Get<TEntity>(id);
            }
        }

        public TEntity Create(TEntity entity)
        {
            using (var session = GetSession())
            {
                session.Save(entity);
            }

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            using (var session = GetSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(entity, entity.Id);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return entity;
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        private static ISessionFactory CreateSessionFactory()
        { 
            return Fluently.Configure()
               .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("SpeedyDonkeyDbContext")))
                    .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(false, true, false))
                    .BuildSessionFactory();
        }
    }
}