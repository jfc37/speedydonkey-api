using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public interface IApiModel<TEntity> where TEntity : IEntity
    {
        string Url { get; set; }

        IEntity ToEntity();

        IApiModel<TEntity> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, TEntity entity);
    }
    public class AccountModel : IAccount, IApiModel<Account>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public IEntity ToEntity()
        {
            return new Account
            {
                Email = Email,
                Password = Password
            };
        }

        public IApiModel<Account> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, Account entity)
        {
            return new AccountModel
            {
                Email = entity.Email,
                Url = urlConstructor.Construct("AccountApi", new { id = entity.Id }, request)
            };
        }
    }
}