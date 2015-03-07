using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class AccountModel : IAccount, IApiModel<Account>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public IUser User { get; set; }
        public string Url { get; set; }
        public Account ToEntity()
        {
            return new Account
            {
                Id = Id,
                Email = Email,
                Password = Password
            };
        }

        public IApiModel<Account> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, Account entity)
        {
            return new AccountModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Url = urlConstructor.Construct("AccountApi", new { id = entity.Id }, request)
            };
        }
    }
}