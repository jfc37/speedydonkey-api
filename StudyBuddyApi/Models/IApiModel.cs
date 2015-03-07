using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public interface IApiModel<TEntity> where TEntity : IEntity
    {
        int Id { get; set; }
        string Url { get; set; }

        TEntity ToEntity();

        IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, TEntity entity);
        IApiModel<TEntity> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id);
    }
}