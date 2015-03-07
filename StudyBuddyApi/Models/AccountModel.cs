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
            var model = new AccountModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Url = ConstructUrl(request, urlConstructor, entity.Id)
            };
            if (entity.User != null)
                model.User = (IUser) new UserModel().CreateModelWithOnlyUrl(request, urlConstructor, entity.User.Id);

            return model;
        }

        public IApiModel<Account> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            return new AccountModel
            {
                Url = ConstructUrl(request, urlConstructor, id)
            };
        }

        private string ConstructUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            return urlConstructor.Construct("AccountApi", new { id }, request);
        }
    }
}