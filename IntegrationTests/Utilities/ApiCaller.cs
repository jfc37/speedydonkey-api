using RestSharp;
using RestSharp.Authenticators;

namespace IntegrationTests.Utilities
{
    public static class ApiCaller
    {
        public static IRestResponse<T> Post<T>(object data, string resource) where T : new()
        {
            var request = CreateRequest(data, resource, Method.POST);
            return Execute<T>(request);
        }
        public static IRestResponse<T> Get<T>(string resource) where T : new()
        {
            var request = CreateRequest(resource, Method.GET);
            return Execute<T>(request);
        }
        public static IRestResponse<T> Delete<T>(string resource) where T : new()
        {
            var request = CreateRequest(resource, Method.DELETE);
            return Execute<T>(request);
        }

        private static IRestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient("http://localhost:50831/api");
            //var client = new RestClient("https://api-speedydonkey.azurewebsites.net/api");
            client.Authenticator = new HttpBasicAuthenticator("joseph@fullswing.co.nz", "password");
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