using System.Collections.Generic;
using System.Linq;

namespace Contracts.Reports.TeacherInvoices
{
    /// <summary>
    /// Teacher invoice report response
    /// </summary>
    public class TeacherInvoiceResponse
    {
        public TeacherInvoiceResponse()
        {
            Lines = new List<TeacherInvoiceLine>();
        }

        public TeacherInvoiceResponse(List<TeacherInvoiceLine> teacherInvoiceLines)
        {
            Lines = teacherInvoiceLines;
        }

        public List<TeacherInvoiceLine> Lines { get; set; }

        public decimal TotalOwed => Lines.Select(x => x.AmountOwed)
            .DefaultIfEmpty(0)
            .Sum();
    }
}