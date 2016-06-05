namespace Contracts.Reports.PassSales
{
    /// <summary>
    /// A line on the pass sales report.
    /// The pass name, number sold, and revenue received.
    /// </summary>
    public class PassSaleLine
    {
        public string Name { get; set; }
        public int NumberSold { get; set; }
        public decimal Revenue { get; set; }

    }
}