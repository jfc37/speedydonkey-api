using RestSharp;
using RestSharp.Authenticators;

namespace IntegrationTests.Utilities
{
    public static class ApiCaller
    {
        public const string StandardAuthenticationEmail = "joseph@fullswing.co.nz";

        public static IRestResponse<T> Put<T>(object data, string resource) where T : new()
        {
            var request = CreateRequest(data, resource, Method.PUT);
            return Execute<T>(request, StandardAuthenticationEmail);
        }
        public static IRestResponse<T> Put<T>(string resource) where T : new()
        {
            var request = CreateRequest(resource, Method.PUT);
            return Execute<T>(request, StandardAuthenticationEmail);
        }
        public static IRestResponse<T> Post<T>(object data, string resource) where T : new()
        {
            var request = CreateRequest(data, resource, Method.POST);
            return Execute<T>(request, StandardAuthenticationEmail);
        }
        public static IRestResponse<T> Post<T>(string resource) where T : new()
        {
            var request = CreateRequest(resource, Method.POST);
            return Execute<T>(request, StandardAuthenticationEmail);
        }

        public static IRestResponse<T> Get<T>(string resource) where T : new()
        {
            return Get<T>(resource, StandardAuthenticationEmail);
        }

        public static IRestResponse<T> Get<T>(string resource, string authenticationEmail) where T : new()
        {
            var request = CreateRequest(resource, Method.GET);
            return Execute<T>(request, authenticationEmail);
        }

        public static IRestResponse<T> Delete<T>(string resource) where T : new()
        {
            var request = CreateRequest(resource, Method.DELETE);
            return Execute<T>(request, StandardAuthenticationEmail);
        }

        private static IRestResponse<T> Execute<T>(RestRequest request, string authenticationEmail) where T : new()
        {
            var client = new RestClient("http://localhost:50831/api");
            //var client = new RestClient("https://api-speedydonkey.azurewebsites.net/api");
            client.Authenticator = new HttpBasicAuthenticator(authenticationEmail, "password");
            return client.Execute<T>(request);
        }

        private static RestRequest CreateRequest(object data, string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.AddJsonBody(data);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        private static RestRequest CreateRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
    }
}