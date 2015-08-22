using System.Net;
using Common;
using Common.Extensions;
using Data.CodeChunks;

namespace OnlinePayments.CodeChunks
{
    public class CreateWebRequestForPoli : ICodeChunk<WebRequest>
    {
        private readonly IAppSettings _appSettings;
        private readonly string _url;

        public CreateWebRequestForPoli(IAppSettings appSettings, string url)
        {
            _appSettings = appSettings;
            _url = url;
        }

        public WebRequest Do()
        {
            var auth = new GetPoliAuthorisation(_appSettings).Do();
            var request = WebRequest.Create(_url);
            request.Headers.Add("Authorization", "Basic " + auth);
            return request;
        }
    }
}