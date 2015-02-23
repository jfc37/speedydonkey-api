using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
using Models;
using Models.Tests.Builders;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Tests.Builders;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests
{
    [TestFixture]
    public class BasicAuthAuthoriseAttributeTestFixture
    {
        private HttpActionContextBuilder _httpActionContextBuilder;
        private MockPrincipalBuilder _principalBuilder;
        private MockDependencyResolverBuilder _dependencyResolverBuilder;
        private MockEntitySearchBuilder<User> _entitySearchBuilder;
        private MockPasswordHasherBuilder _passwordHasherBuilder;

        [SetUp]
        public void Setup()
        {
            _httpActionContextBuilder = new HttpActionContextBuilder();
            _principalBuilder = new MockPrincipalBuilder();
            _entitySearchBuilder = new MockEntitySearchBuilder<User>()
                .WithEntity(new UserBuilder().WithUsername("username").WithPassword("password").Build());
            _passwordHasherBuilder = new MockPasswordHasherBuilder();
            _dependencyResolverBuilder = new MockDependencyResolverBuilder()
                .WithService(_entitySearchBuilder.BuildObject())
                .WithService(_passwordHasherBuilder.BuildObject());
             _principalBuilder
                .WithNoLoggedOnUser()
                .BuildObject();

            string credentials = string.Format("{0}:{1}", "username", "password");
            string encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(credentials));
            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", encodedCredentials));
        }

        private BasicAuthAuthoriseAttribute GetBasicAuthAuthoriseAttribute()
        {
            return new BasicAuthAuthoriseAttribute();
        }

        [Test]
        public void It_should_be_authenticated_when_current_principal_is_authenticated()
        {
             _principalBuilder
                .WithLoggedOnUser()
                .BuildObject();

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.IsNull(_httpActionContextBuilder.Build().Response);
        }

        [Test]
        public void It_should_be_unauthenticated_when_authorisation_header_scheme_isnt_basic()
        {
            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("not_basic"));

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_no_authorisation_headers_included()
        {
            _httpActionContextBuilder.WithNoAuthorisationHeader();

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_credentials_not_included()
        {
            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic"));

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_credentials_are_not_encoded_correctly()
        {
            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", "username-password"));

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_credentials_are_not_in_correct_format()
        {
            string credentials = string.Format("{0}-{1}", "username", "password");
            string encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(credentials));
            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", encodedCredentials));

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_no_matching_username_found()
        {
            _entitySearchBuilder.WithNoResults();

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_unauthenticated_when_credentials_are_incorrect()
        {
            _passwordHasherBuilder.WithFailedPasswordValidation();

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
        }

        [Test]
        public void It_should_be_authenticated_when_credentials_are_correct()
        {
            _passwordHasherBuilder.WithSuccessfulPasswordValidation();

            var authoriseFilter = GetBasicAuthAuthoriseAttribute();
            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

            Assert.IsNull(_httpActionContextBuilder.Build().Response);
        }
    }

    public class MockEntitySearchBuilder<T> : MockBuilder<IEntitySearch<T>> where T : class
    {
        private IList<T> _entities;

        public MockEntitySearchBuilder()
        {
            _entities = new List<T>();
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(_entities);
        }

        public MockEntitySearchBuilder<T> WithEntity(T entity)
        {
            _entities.Add(entity);
            return this;
        }

        public MockEntitySearchBuilder<T> WithNoResults()
        {
            _entities.Clear();;
            return this;
        }
    }
}
