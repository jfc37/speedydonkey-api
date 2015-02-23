using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class HttpActionContextBuilder
    {
        private HttpActionContext _httpActionContext;

        public HttpActionContextBuilder()
        {
            _httpActionContext = new HttpActionContext
            {
                ControllerContext = new HttpControllerContext
                {
                    Request = new HttpRequestMessage(),
                    ControllerDescriptor = new HttpControllerDescriptor()
                },
                ActionDescriptor = new ReflectedHttpActionDescriptor
                {
                    Configuration = new HttpConfiguration()
                }
            };
        }

        public HttpActionContextBuilder WithNoAuthorisationHeader()
        {
            _httpActionContext.ControllerContext.Request.Headers.Authorization = null;
            return this;
        }

        public HttpActionContext Build()
        {
            return _httpActionContext;
        }

        public HttpActionContextBuilder WithAuthorisationHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            _httpActionContext.ControllerContext.Request.Headers.Authorization = authenticationHeaderValue;
            return this;
        }
    }
}