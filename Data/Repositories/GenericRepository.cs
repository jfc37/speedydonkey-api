using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

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
        private readonly ISpeedyDonkeyDbContext _context;

        protected GenericRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public TEntity Get(int id)
        {
            return GetDatabaseSet().SingleOrDefault(x => x.Id == id);
        }

        public TEntity Create(TEntity entity)
        {
            GetDatabaseSet().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        private IDbSet<TEntity> GetDatabaseSet()
        {
            return _context.GetSetOfType<TEntity>();
        }
    }
}