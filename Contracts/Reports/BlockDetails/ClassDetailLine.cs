namespace Contracts.Reports.BlockDetails
{
    /// <summary>
    /// A line on the block details report.
    /// </summary>
    public class ClassDetailLine
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
        public int Attendance { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expense { get; set; }
        public decimal Profit => Revenue - Expense;
    }
}