using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ExtractLoggedInUser : ICodeChunk<User>
    {
        private readonly HttpRequestMessage _request;

        public ExtractLoggedInUser(HttpRequestMessage request)
        {
            _request = request;
        }

        public User Do()
        {
            var authHeader = _request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&  !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var email = GetEmail(authHeader);
                    var userSearch = (IRepository<User>)_request.GetDependencyScope().GetService(typeof(IRepository<User>));
                    var user = userSearch.GetAll()
                        .SingleOrDefault(x => x.Email == email);

                    return user;
                }
            }

            return null;
        }

        private string GetEmail(AuthenticationHeaderValue authHeader)
        {
            var rawCredentials = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            string credentials;
            try
            {
                credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
            }
            catch (FormatException)
            {
                return "";
            }
            var split = credentials.Split(':');
            if (split.Count() != 2)
                return "";
            return split[0];
        }
    }
}