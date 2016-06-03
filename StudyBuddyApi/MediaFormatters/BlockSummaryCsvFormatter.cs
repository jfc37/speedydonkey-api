using Contracts.Reports.BlockSummary;
using CsvHelper;

namespace SpeedyDonkeyApi.MediaFormatters
{
    /// <summary>
    /// Block Summary CSV formatter
    /// </summary>
    public class BlockSummaryCsvFormatter : CsvFormatter<BlockSummaryResponse>
    {
        protected override void WriteCsvHeader(CsvWriter writer)
        {
            writer.WriteField("Name");
            writer.WriteField("Attendance");
            writer.WriteField("Revenue");
        }

        protected override void WriteCsvBody(CsvWriter writer, BlockSummaryResponse value)
        {
            foreach (var line in value.Lines)
            {
                writer.WriteField(line.Name);
                writer.WriteField(line.Attendance);
                writer.WriteField(line.Revenue);
                writer.NextRecord();
            }
        }

        protected override string GetFileName()
        {
            return "BlockSummary";
        }
    }
}