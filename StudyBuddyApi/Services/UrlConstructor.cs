using System.Net.Http;
using System.Web.Http.Routing;

namespace SpeedyDonkeyApi.Services
{
    public interface IUrlConstructor
    {
        string Construct(string routeName, object parameters, HttpRequestMessage request);
    }

    public class UrlConstructor : IUrlConstructor
    {
        private UrlHelper _urlHelper;

        public UrlConstructor()
        {
            _urlHelper = new UrlHelper();
        }

        public string Construct(string routeName, object parameters, HttpRequestMessage request)
        {
            _urlHelper.Request = request;
            return _urlHelper.Link(routeName, parameters);
        }
    }
}