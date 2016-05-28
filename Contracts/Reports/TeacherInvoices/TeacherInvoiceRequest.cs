using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Reports.TeacherInvoices
{
    /// <summary>
    /// Teacher invoice report request
    /// </summary>
    public class TeacherInvoiceRequest
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}