using Common.Extensions.DateTimes;
using Contracts.Reports.TeacherInvoices;
using FluentValidation;
using Validation.Rules;

namespace Validation.Validators.Reports
{
    /// <summary>
    /// Validator for DateRangeReportRequest
    /// </summary>
    public class DateRangeReportRequestValidator : AbstractValidator<DateRangeReportRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateRangeReportRequestValidator"/> class.
        /// </summary>
        public DateRangeReportRequestValidator()
        {
            RuleFor(x => x.From)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage("Please provide a from date");

            RuleFor(x => x.To)
                .Must(x => new DateIsNotTooFarInThePastRule(x).IsValid()).WithMessage("Please provide a to date")
                .Must((x,y) => x.From.IsOnOrBefore(x.To)).WithMessage("Please provide a to date on or afer the from date");
        }
    }
}
