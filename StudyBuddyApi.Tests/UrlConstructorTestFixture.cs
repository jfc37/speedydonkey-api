using NUnit.Framework;
using SpeedyDonkeyApi.Services;
using SpeedyDonkeyApi.Tests.Builders;

namespace SpeedyDonkeyApi.Tests
{
    [TestFixture]
    public class UrlConstructorTestFixture
    {
        private UrlConstructorBuilder _urlConstructorBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _urlConstructorBuilder = new UrlConstructorBuilder();
        }

        private UrlConstructor BuildUrlConstructor()
        {
            return _urlConstructorBuilder.Build();
        }

        public class Construct : UrlConstructorTestFixture
        {
            private string _routeName;
            private object _parameters;
            private HttpRequestMessageBuilder _httpRequestMessageBuilder;

            [SetUp]
            public void Setup()
            {
                _httpRequestMessageBuilder = new HttpRequestMessageBuilder();
                _routeName = "route_name";
                _parameters = new {id = "blah", parent_id = "parent"};
            }

            [Test]
            public void It_should_return_url_constructed_from_parameters()
            {
                string routeTemplate = "something/{id}/{face}";
                _httpRequestMessageBuilder
                    .WithBaseUrl("http://www.base.com")
                    .WithRoute(_routeName, routeTemplate);
                _parameters = new {id = 55, face = "happy"};

                var urlConstructor = BuildUrlConstructor();
                var url = urlConstructor.Construct(_routeName, _parameters, _httpRequestMessageBuilder.Build());

                Assert.AreEqual("http://www.base.com/something/55/happy", url);
            }
        } 
    }
}
