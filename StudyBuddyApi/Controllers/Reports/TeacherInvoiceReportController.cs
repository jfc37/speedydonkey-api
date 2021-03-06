using System.Web.Http;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports;
using Models;
using SpeedyDonkeyApi.Filter;
using Validation;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/reports/teacher-invoices")]
    public class TeacherInvoiceReportController : ReportController<DateRangeReportRequest, TeacherInvoiceResponse>
    {
        private readonly ITeacherInvoiceReportGenerator _reportGenerator;

        public TeacherInvoiceReportController(
            IValidatorOverlord validatorOverlord,
            ITeacherInvoiceReportGenerator reportGenerator) 
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