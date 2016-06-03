using System.Web.Http;
using Contracts.Reports.BlockDetails;
using Core.Queries.Reports;
using Models;
using SpeedyDonkeyApi.Filter;
using Validation;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/reports/block-details")]
    public class BlockDetailsReportController : ReportController<BlockDetailsRequest, BlockDetailsResponse>
    {
        private readonly IBlockDetailsReportGenerator _reportGenerator;

        public BlockDetailsReportController(
            IValidatorOverlord validatorOverlord,
            IBlockDetailsReportGenerator reportGenerator) 
            : base(validatorOverlord)
        {
            _reportGenerator = reportGenerator;
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get([FromUri] BlockDetailsRequest request)
        {
            return RunReport(request, x => _reportGenerator.Create(x));
        }
    }
}