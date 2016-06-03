using Contracts.Reports.BlockDetails;

namespace Core.Queries.Reports
{
    /// <summary>
    /// Generates block details reports
    /// </summary>
    public interface IBlockDetailsReportGenerator
    {
        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        BlockDetailsResponse Create(BlockDetailsRequest request);
    }
}