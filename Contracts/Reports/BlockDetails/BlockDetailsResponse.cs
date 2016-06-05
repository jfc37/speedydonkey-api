using System.Collections.Generic;
using System.Linq;

namespace Contracts.Reports.BlockDetails
{
    /// <summary>
    /// Block details report response
    /// </summary>
    public class BlockDetailsResponse
    {
        public BlockDetailsResponse()
        {
            Lines = new List<ClassDetailLine>();
        }

        public BlockDetailsResponse(List<ClassDetailLine> lines)
        {
            Lines = lines;
        }

        public List<ClassDetailLine> Lines { get; set; }

        public int TotalAttendance => Lines.Select(x => x.Attendance)
            .DefaultIfEmpty(0)
            .Sum();

        public decimal TotalRevenue => Lines.Select(x => x.Revenue)
            .DefaultIfEmpty(0)
            .Sum();
        
        public decimal TotalExpense => Lines.Select(x => x.Expense)
            .DefaultIfEmpty(0)
            .Sum();

        public decimal TotalProfit => TotalRevenue - TotalExpense;
    }
}