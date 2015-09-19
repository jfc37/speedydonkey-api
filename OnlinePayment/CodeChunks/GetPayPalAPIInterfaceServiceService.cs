using Common;
using Data.CodeChunks;
using PayPal.PayPalAPIInterfaceService;

namespace OnlinePayments.CodeChunks
{
    public class GetPayPalAPIInterfaceServiceService : ICodeChunk<PayPalAPIInterfaceServiceService>
    {
        private readonly IAppSettings _appSettings;

        public GetPayPalAPIInterfaceServiceService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public PayPalAPIInterfaceServiceService Do()
        {
            var config = new GetPayPalConfig(_appSettings).Do();

            return new PayPalAPIInterfaceServiceService(config);
        }
    }
}