using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Web.Http;
using System.Web.Http.Controllers;
using NSubstitute;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class HttpActionContextBuilder
    {
        private HttpActionContext _httpActionContext;

        public HttpActionContextBuilder()
        {
            var attributes = new Collection<AllowAnonymousAttribute>();

            var controllerDescriptor = Substitute.For<HttpControllerDescriptor>();
            controllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Returns(attributes);

            var controllerContext = new HttpControllerContext
            {
                Request = new HttpRequestMessage(),
                ControllerDescriptor = controllerDescriptor
            };

            var actionDescriptor = Substitute.For<HttpActionDescriptor>();
            actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                .Returns(attributes);


            _httpActionContext = new HttpActionContext(controllerContext, actionDescriptor);

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

        public HttpActionContextBuilder WithAllowAnonymous()
        {
            var attributes = new Collection<AllowAnonymousAttribute>
            {
                new AllowAnonymousAttribute()
            };

            var controllerDescriptor = Substitute.For<HttpControllerDescriptor>();
            controllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Returns(attributes);

            var controllerContext = new HttpControllerContext
            {
                Request = new HttpRequestMessage(),
                ControllerDescriptor = controllerDescriptor
            };

            var actionDescriptor = Substitute.For<HttpActionDescriptor>();
            actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                .Returns(attributes);

            _httpActionContext = new HttpActionContext(controllerContext, actionDescriptor);
            return this;
        }
    }
}