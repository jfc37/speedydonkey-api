using System;
using System.Net.Http;
using System.Web.Http;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class HttpRequestMessageBuilder
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public HttpRequestMessageBuilder()
        {
            _httpRequestMessage = new HttpRequestMessage();
        }

        public HttpRequestMessage Build()
        {
            _httpRequestMessage.Content = new StringContent("blah");
            _httpRequestMessage.Method = new HttpMethod("GET");
            return _httpRequestMessage;
        }

        public HttpRequestMessageBuilder WithRoute(string routeName, string routeTemplate)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(routeName, routeTemplate);
            _httpRequestMessage.SetConfiguration(configuration);
            return this;
        }

        public HttpRequestMessageBuilder WithBaseUrl(string baseUrl)
        {
            _httpRequestMessage.RequestUri = new Uri(baseUrl);
            return this;
        }

        public HttpRequestMessageBuilder WithResponseAttached()
        {
            _httpRequestMessage.SetConfiguration(new HttpConfiguration());
            return this;
        }
    }
}