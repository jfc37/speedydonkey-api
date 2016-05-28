namespace Contracts.Reports.TeacherInvoices
{
    /// <summary>
    /// A line on the teacher invoice report.
    /// The teacher, and how much they are owed.
    /// </summary>
    public class TeacherInvoiceLine
    {
        public string Name { get; set; }
        public decimal AmountOwed { get; set; }
    }
}