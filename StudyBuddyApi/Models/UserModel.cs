using System.Collections.Generic;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserModel : IUser, IApiModel<User>
    {
        public IAccount Account { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public ISchedule Schedule { get; set; }
        public IList<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }

        public UserModel()
        {
            Account = new AccountModel();
        }

        public User ToEntity()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                Surname = Surname,
                Account = ((IApiModel<Account>) Account).ToEntity()
            };
        }

        public IApiModel<User> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                Url = urlConstructor.Construct("UserApi", new {id = entity.Id}, request)
            };
        }
    }
}