using System.Web.Http;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports.TeacherInvoices;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/reports/teacher-invoices")]
    public class TeacherInvoiceReportController : BaseApiController
    {
        private readonly ITeacherInvoiceReportGenerator _reportGenerator;

        public TeacherInvoiceReportController(ITeacherInvoiceReportGenerator reportGenerator)
        {
            _reportGenerator = reportGenerator;
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get([FromUri] TeacherInvoiceRequest request)
        {
            var report = _reportGenerator.Create(request);
            return Ok(report);
        }
    }
}