using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class UserBuilder
    {
        private string _username;
        private string _password;
        private int _id;
        private Person _person;

        public UserBuilder WithValidInputs()
        {
            _username = "username";
            _password = "password";
            _id = 5345;
            return this;
        }

        public User Build()
        {
            return new User
            {
                Username = _username,
                Password = _password,
                Id = _id,
                Person = _person
            };
        }

        public UserModel BuildModel()
        {
            return new UserModel
            {
                Username = _username,
                Password = _password,
                Id = _id
            };
        }

        public UserBuilder WithUsername(string username)
        {
            _username = username;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithPerson(Person person)
        {
            _person = person;
            return this;
        }
    }
}