using Contracts.Reports.BlockSummary;
using Contracts.Reports.TeacherInvoices;

namespace Core.Queries.Reports
{
    /// <summary>
    /// Generates block summary reports
    /// </summary>
    public interface IBlockSummaryReportGenerator
    {
        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        BlockSummaryResponse Create(DateRangeReportRequest request);
    }
}