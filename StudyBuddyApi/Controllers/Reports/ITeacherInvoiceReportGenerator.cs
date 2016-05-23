using Contracts.Reports.TeacherInvoices;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    /// <summary>
    /// Generates teacher invoice reports
    /// </summary>
    public interface ITeacherInvoiceReportGenerator
    {
        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        TeacherInvoiceResponse Create(TeacherInvoiceRequest request);
    }
}