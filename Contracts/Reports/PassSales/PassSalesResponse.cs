using System.Collections.Generic;
using System.Linq;

namespace Contracts.Reports.PassSales
{
    /// <summary>
    /// Pass sales report response
    /// </summary>
    public class PassSalesResponse
    {
        public PassSalesResponse()
        {
            Lines = new List<PassSaleLine>();
        }

        public PassSalesResponse(List<PassSaleLine> lines)
        {
            Lines = lines;
        }

        public List<PassSaleLine> Lines { get; set; }

        public decimal TotalSold => Lines.Select(x => x.NumberSold)
            .DefaultIfEmpty(0)
            .Sum();

        public decimal TotalRevenue => Lines.Select(x => x.Revenue)
            .DefaultIfEmpty(0)
            .Sum();
    }
}