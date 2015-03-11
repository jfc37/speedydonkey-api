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
        IApiModel<TEntity> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id);
    }

    public abstract class ApiModel<TEntity, TModel> : IApiModel<TEntity>
        where TEntity : IEntity, new()
        where TModel : class, IApiModel<TEntity>, new()
    {
        public int Id { get; set; }
        public string Url { get; set; }
        protected abstract string RouteName { get; }

        public TEntity ToEntity(ICommonInterfaceCloner cloner)
        {
            var entity = cloner.Clone<TModel, TEntity>(this as TModel);
            return entity;
        }

        public IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor,
            TEntity entity,
            ICommonInterfaceCloner cloner)
        {
            var model = cloner.Clone<TEntity, TModel>(entity);
            model.Url = urlConstructor.Construct(RouteName, new {id = entity.Id}, request);

            AddChildUrls(request, urlConstructor, entity, model);

            return model;
        }

        protected virtual void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, TEntity entity, TModel model)
        {
        }

        public IApiModel<TEntity> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            return new TModel
            {
                Url = urlConstructor.Construct(RouteName, new {id}, request)
            };
        }
    }
}