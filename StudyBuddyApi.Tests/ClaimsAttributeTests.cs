//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Text;
//using Common.Tests.Builders.MockBuilders;
//using Data.Searches;
//using Models;
//using Moq;
//using NUnit.Framework;
//using SpeedyDonkeyApi.Filter;
//using SpeedyDonkeyApi.Tests.Builders;
//using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

//namespace SpeedyDonkeyApi.Tests
//{
//    [TestFixture]
//    public class ClaimsAttributeTests
//    {
//        private HttpActionContextBuilder _httpActionContextBuilder;
//        private MockPrincipalBuilder _principalBuilder;
//        private MockDependencyResolverBuilder _dependencyResolverBuilder;
//        private MockEntitySearchBuilder<User> _entitySearchBuilder;
//        private MockPasswordHasherBuilder _passwordHasherBuilder;

//        [SetUp]
//        public void Setup()
//        {
//            _httpActionContextBuilder = new HttpActionContextBuilder();
//            _principalBuilder = new MockPrincipalBuilder();
//            _entitySearchBuilder = new MockEntitySearchBuilder<User>()
//                .WithEntity(new User{Email = "email", Password = "password"});
//            _passwordHasherBuilder = new MockPasswordHasherBuilder();
//            _dependencyResolverBuilder = new MockDependencyResolverBuilder()
//                .WithService(_entitySearchBuilder.BuildObject())
//                .WithService(_passwordHasherBuilder.BuildObject());
//            _principalBuilder
//               .WithNoLoggedOnUser()
//               .BuildObject();

//            string credentials = string.Format("{0}:{1}", "username", "password");
//            string encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(credentials));
//            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", encodedCredentials));
//        }

//        private ClaimsAuthorise GetClaimsAuthoriseAttribute()
//        {
//            return new ClaimsAuthorise();
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_authorisation_header_scheme_isnt_basic()
//        {
//            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("not_basic"));

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_no_authorisation_headers_included()
//        {
//            _httpActionContextBuilder.WithNoAuthorisationHeader();

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_credentials_not_included()
//        {
//            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic"));

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_credentials_are_not_encoded_correctly()
//        {
//            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", "username-password"));

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_credentials_are_not_in_correct_format()
//        {
//            string credentials = string.Format("{0}-{1}", "username", "password");
//            string encodedCredentials = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(credentials));
//            _httpActionContextBuilder.WithAuthorisationHeader(new AuthenticationHeaderValue("basic", encodedCredentials));

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_no_matching_username_found()
//        {
//            _entitySearchBuilder.WithNoResults();

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_unauthenticated_when_credentials_are_incorrect()
//        {
//            _passwordHasherBuilder.WithFailedPasswordValidation();

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.AreEqual(HttpStatusCode.Unauthorized, _httpActionContextBuilder.Build().Response.StatusCode);
//        }

//        [Test]
//        public void It_should_be_authenticated_when_credentials_are_correct()
//        {
//            _passwordHasherBuilder.WithSuccessfulPasswordValidation();

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.IsNull(_httpActionContextBuilder.Build().Response);
//        }

//        [Test]
//        public void It_should_be_authenticated_when_allow_anonymous_is_present()
//        {
//            _httpActionContextBuilder.WithAllowAnonymous();

//            var authoriseFilter = GetClaimsAuthoriseAttribute();
//            _httpActionContextBuilder.WithAllowAnonymous();

//            authoriseFilter.OnAuthorization(_httpActionContextBuilder.Build());

//            Assert.IsNull(_httpActionContextBuilder.Build().Response);
//        }
//    }
//}
