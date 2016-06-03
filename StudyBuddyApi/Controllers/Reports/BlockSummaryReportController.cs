using System.Web.Http;
using Contracts.Reports.BlockSummary;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports;
using Models;
using SpeedyDonkeyApi.Filter;
using Validation;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/reports/block-summary")]
    public class BlockSummaryReportController : ReportController<DateRangeReportRequest, BlockSummaryResponse>
    {
        private readonly IBlockSummaryReportGenerator _reportGenerator;

        public BlockSummaryReportController(
            IValidatorOverlord validatorOverlord,
            IBlockSummaryReportGenerator reportGenerator) 
            : base(validatorOverlord)
        {
            _reportGenerator = reportGenerator;
        }

        [Route]
        //[ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get([FromUri] DateRangeReportRequest request)
        {
            return RunReport(request, x => _reportGenerator.Create(x));
        }
    }
}