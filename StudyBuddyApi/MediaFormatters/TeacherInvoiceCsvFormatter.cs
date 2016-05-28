using Contracts.Reports.TeacherInvoices;
using CsvHelper;

namespace SpeedyDonkeyApi.MediaFormatters
{
    public class TeacherInvoiceCsvFormatter : CsvFormatter<TeacherInvoiceResponse>
    {
        protected override void WriteCsvHeader(CsvWriter writer)
        {
            writer.WriteField("Name");
            writer.WriteField("Amount Owed");
        }

        protected override void WriteCsvBody(CsvWriter writer, TeacherInvoiceResponse value)
        {
            foreach (var line in value.Lines)
            {
                writer.WriteField(line.Name);
                writer.WriteField(line.AmountOwed);
                writer.NextRecord();
            }
        }

        protected override string GetFileName()
        {
            return "TeacherInvoices";
        }
    }
}