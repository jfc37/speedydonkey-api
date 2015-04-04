using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public interface IApiModel<TEntity> where TEntity : IEntity
    {
        int Id { get; set; }
        string Url { get; set; }

        TEntity ToEntity(ICommonInterfaceCloner cloner);

        IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, TEntity entity, ICommonInterfaceCloner cloner);
    }

    public abstract class ApiModel<TEntity, TModel> : IApiModel<TEntity>
        where TEntity : IEntity, new()
        where TModel : class, IApiModel<TEntity>, new()
    {
        public int Id { get; set; }
        public string Url { get; set; }
        protected abstract string RouteName { get; }

        public virtual TEntity ToEntity(ICommonInterfaceCloner cloner)
        {
            var entity = cloner.Clone<TModel, TEntity>(this as TModel);
            AddChildrenToEntity(entity, cloner);
            return entity;
        }

        protected virtual void AddChildrenToEntity(TEntity entity, ICommonInterfaceCloner cloner)
        {
            
        }

        public virtual IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor,
            TEntity entity,
            ICommonInterfaceCloner cloner)
        {
            var model = cloner.Clone<TEntity, TModel>(entity);
            model.Url = urlConstructor.Construct(RouteName, new {id = entity.Id}, request);
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