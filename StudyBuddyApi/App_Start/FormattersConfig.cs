using System.Web.Http;
using SpeedyDonkeyApi.MediaFormatters;

namespace SpeedyDonkeyApi
{
    public static class FormattersConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Add(new TeacherInvoiceCsvFormatter());
            config.Formatters.Add(new PassSalesCsvFormatter());
            config.Formatters.Add(new BlockSummaryCsvFormatter());
            config.Formatters.Add(new BlockDetailsCsvFormatter());
        }
    }
}