using System.Collections.Generic;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserModel : IUser, IApiModel<User>
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public ISchedule Schedule { get; set; }
        public IList<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }


        public User ToEntity()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                Surname = Surname,
                Email = Email,
                Password = Password
            };
        }

        public IApiModel<User> CloneFromEntity(HttpRequestMessage request, IUrlConstructor urlConstructor, User entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                Email = entity.Email,
                Url = ConstructUrl(request, urlConstructor, entity.Id)
            };
        }

        public IApiModel<User> CreateModelWithOnlyUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            return new UserModel
            {
                Url = ConstructUrl(request, urlConstructor, id),
            };
        }

        private string ConstructUrl(HttpRequestMessage request, IUrlConstructor urlConstructor, int id)
        {
            return urlConstructor.Construct("UserApi", new { id }, request);
        }
    }
}