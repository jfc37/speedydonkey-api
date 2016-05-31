using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Reports.TeacherInvoices
{
    /// <summary>
    /// Date range report request
    /// </summary>
    public class DateRangeReportRequest
    {
        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}