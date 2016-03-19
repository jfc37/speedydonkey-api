using System;
using AuthZero.Interfaces;
using Models;

namespace AuthZero.Domain.Clients
{
    /// <summary>
    /// A fake auth0 client repository
    /// </summary>
    /// <seealso cref="IAuthZeroClientRepository" />
    public class FakeAuthZeroClientRepository : IAuthZeroClientRepository
    {
        public string Create(User user)
        {
            return Guid.NewGuid().ToString();
        }

        public User Get(string globalId)
        {
            var email = globalId == "auth0|56e641bb5d3ca9ae1853a9d2"
                ? "placid.joe@gmail.com"
                : $"user_{Guid.NewGuid()}@email.com";

            return new User
            {
                Email = email,
                GlobalId = globalId
            };
        }

        public void Delete(string globalId)
        {

        }
    }
}