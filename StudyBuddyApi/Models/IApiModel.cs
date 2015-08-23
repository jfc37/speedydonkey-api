using System.Net.Http;
using Common;

namespace SpeedyDonkeyApi.Models
{
    public interface IApiModel<TEntity> where TEntity : IEntity
    {
        int Id { get; set; }

        TEntity ToEntity();

        IApiModel<TEntity> CloneFromEntity(TEntity entity);
        IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, TEntity entity);
    }

    public abstract class ApiModel<TEntity, TModel> : IApiModel<TEntity>
        where TEntity : IEntity, new()
        where TModel : class, IApiModel<TEntity>, new()
    {
        public int Id { get; set; }

        public virtual TEntity ToEntity()
        {
            var entity = new CommonInterfaceCloner().Clone<TModel, TEntity>(this as TModel);
            AddChildrenToEntity(entity);
            return entity;
        }

        protected virtual void AddChildrenToEntity(TEntity entity)
        {
            
        }

        public virtual IApiModel<TEntity> CloneFromEntity(TEntity entity)
        {
            var model = new CommonInterfaceCloner().Clone<TEntity, TModel>(entity);
            AddChildrenToModel(entity, model);
            SanitiseModel(model);
            return model;
        }

        public virtual IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request,
            TEntity entity)
        {
            var model = new CommonInterfaceCloner().Clone<TEntity, TModel>(entity);
            AddChildrenToModel(entity, model);
            SanitiseModel(model);
            return model;
        }

        protected virtual void SanitiseModel(TModel model)
        {
            
        }

        protected virtual void AddChildrenToModel(TEntity entity, TModel model) { }
    }
}