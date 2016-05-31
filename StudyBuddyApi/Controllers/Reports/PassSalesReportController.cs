using System.Web.Http;
using Contracts.Reports.PassSales;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports.PassSales;
using Models;
using SpeedyDonkeyApi.Filter;
using Validation;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/reports/pass-sales")]
    public class PassSalesReportController : ReportController<DateRangeReportRequest, PassSalesResponse>
    {
        private readonly IPassSalesReportGenerator _reportGenerator;

        public PassSalesReportController(
            IValidatorOverlord validatorOverlord,
            IPassSalesReportGenerator reportGenerator) 
            : base(validatorOverlord)
        {
            _reportGenerator = reportGenerator;
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get([FromUri] DateRangeReportRequest request)
        {
            return RunReport(request, x => _reportGenerator.Create(x));
        }
    }
}