using Contracts.Reports.PassSales;
using CsvHelper;

namespace SpeedyDonkeyApi.MediaFormatters
{
    /// <summary>
    /// Pass Sales CSV formatter
    /// </summary>
    public class PassSalesCsvFormatter : CsvFormatter<PassSalesResponse>
    {
        protected override void WriteCsvHeader(CsvWriter writer)
        {
            writer.WriteField("Name");
            writer.WriteField("Number Sold");
            writer.WriteField("Revenue");
        }

        protected override void WriteCsvBody(CsvWriter writer, PassSalesResponse value)
        {
            foreach (var line in value.Lines)
            {
                writer.WriteField(line.Name);
                writer.WriteField(line.NumberSold);
                writer.WriteField(line.Revenue);
                writer.NextRecord();
            }
        }

        protected override string GetFileName()
        {
            return "PassSales";
        }
    }
}