using System.Collections.Generic;
using System.Linq;

namespace Contracts.Reports.BlockSummary
{
    /// <summary>
    /// Block summary report response
    /// </summary>
    public class BlockSummaryResponse
    {
        public BlockSummaryResponse()
        {
            Lines = new List<BlockDetailLine>();
        }

        public BlockSummaryResponse(List<BlockDetailLine> lines)
        {
            Lines = lines;
        }

        public List<BlockDetailLine> Lines { get; set; }

        public int TotalAttendance => Lines.Select(x => x.Attendance)
            .DefaultIfEmpty(0)
            .Sum();

        public decimal TotalRevenue => Lines.Select(x => x.Revenue)
            .DefaultIfEmpty(0)
            .Sum();
    }
}