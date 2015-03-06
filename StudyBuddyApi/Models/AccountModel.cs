using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public interface IApiModel
    {
        string Url { get; set; }

        IEntity ToEntity();

        IApiModel CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, IEntity entity);
    }
    public class AccountModel : IAccount, IApiModel
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

        public IApiModel CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, IEntity entity)
        {
            return new AccountModel
            {
                Email = Email,
                Password = Password,
                Url = urlConstructor.Construct("AccountApi", new { userId = 1 }, request)
            };
        }
    }
}