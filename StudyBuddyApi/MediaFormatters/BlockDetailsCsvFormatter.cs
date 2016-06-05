using Contracts.Reports.BlockDetails;
using CsvHelper;

namespace SpeedyDonkeyApi.MediaFormatters
{
    /// <summary>
    /// Block Details CSV formatter
    /// </summary>
    public class BlockDetailsCsvFormatter : CsvFormatter<BlockDetailsResponse>
    {
        protected override void WriteCsvHeader(CsvWriter writer)
        {
            writer.WriteField("Name");
            writer.WriteField("Attendance");
            writer.WriteField("Revenue");
            writer.WriteField("Expense");
            writer.WriteField("Profit");
        }

        protected override void WriteCsvBody(CsvWriter writer, BlockDetailsResponse value)
        {
            foreach (var line in value.Lines)
            {
                writer.WriteField(line.Name);
                writer.WriteField(line.Attendance);
                writer.WriteField(line.Revenue);
                writer.WriteField(line.Expense);
                writer.WriteField(line.Profit);
                writer.NextRecord();
            }
        }

        protected override string GetFileName()
        {
            return "BlockDetails";
        }
    }
}