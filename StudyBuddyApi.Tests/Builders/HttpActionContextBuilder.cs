using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using NSubstitute;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class HttpActionContextBuilder
    {
        private HttpControllerDescriptor _controllerDescriptor;
        private HttpActionDescriptor _actionDescriptor;
        private AuthenticationHeaderValue _authorisation;
        private HttpActionContext _httpActionContext;

        public HttpActionContextBuilder()
        {
            var attributes = new Collection<AllowAnonymousAttribute>();

            _controllerDescriptor = Substitute.For<HttpControllerDescriptor>();
            _controllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Returns(attributes);

            _actionDescriptor = Substitute.For<HttpActionDescriptor>();
            _actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                .Returns(attributes);
        }

        public HttpActionContextBuilder WithNoAuthorisationHeader()
        {
            _authorisation = null;
            return this;
        }

        public HttpActionContext Build()
        {
            if (_httpActionContext == null)
            {
                var controllerContext = new HttpControllerContext
                {
                    Request = new HttpRequestMessage(),
                    ControllerDescriptor = _controllerDescriptor
                };
                controllerContext.Request.Headers.Authorization = _authorisation;

                _httpActionContext = new HttpActionContext(controllerContext, _actionDescriptor);
            }
            return _httpActionContext;
        }

        public HttpActionContextBuilder WithAuthorisationHeader(AuthenticationHeaderValue authenticationHeaderValue)
        {
            _authorisation = authenticationHeaderValue;
            return this;
        }

        public HttpActionContextBuilder WithAllowAnonymous()
        {
            var attributes = new Collection<AllowAnonymousAttribute>
            {
                new AllowAnonymousAttribute()
            };

            _controllerDescriptor = Substitute.For<HttpControllerDescriptor>();
            _controllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Returns(attributes);
            
            _actionDescriptor = Substitute.For<HttpActionDescriptor>();
            _actionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
                .Returns(attributes);

            return this;
        }

        public HttpActionContextBuilder WithDependencyResolver(IDependencyResolver dependencyResolver)
        {
            //var requestSubstitute = Substitute.For<HttpRequestMessage>();
            //requestSubstitute.GetDependencyScope().Returns(dependencyResolver);
            //_httpActionContext.Request = requestSubstitute;
            return this;
        }
    }
}