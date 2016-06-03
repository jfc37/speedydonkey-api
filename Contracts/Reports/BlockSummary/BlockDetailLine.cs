namespace Contracts.Reports.BlockSummary
{
    /// <summary>
    /// A line on the block summary report.
    /// </summary>
    public class BlockDetailLine
    {
        public int BlockId { get; set; }
        public string Name { get; set; }
        public int Attendance { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal Profit => Revenue - Expenses;
    }
}