using Contracts.Reports.PassSales;
using Contracts.Reports.TeacherInvoices;

namespace Core.Queries.Reports.PassSales
{
    /// <summary>
    /// Generates pass sales reports
    /// </summary>
    public interface IPassSalesReportGenerator
    {
        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        PassSalesResponse Create(DateRangeReportRequest request);
    }
}