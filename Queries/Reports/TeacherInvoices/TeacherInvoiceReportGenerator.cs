using System.Linq;
using Common.Extensions;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports.TeacherInvoices;
using Data.Repositories;
using Models;
using NHibernate.Linq;

namespace Queries.Reports.TeacherInvoices
{
    /// <summary>
    /// Generates teacher invoice reports
    /// </summary>
    public class TeacherInvoiceReportGenerator : ITeacherInvoiceReportGenerator
    {
        private readonly IRepository<Event> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherInvoiceReportGenerator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TeacherInvoiceReportGenerator(IRepository<Event> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public TeacherInvoiceResponse Create(DateRangeReportRequest request)
        {
            request.GuardAgainstNull(nameof(request));

            var eventsInRange = _repository.Queryable()
                .FetchMany(x => x.Teachers)
                .ThenFetch(x => x.User)
                .Where(x => x.StartTime >= request.From)
                .Where(x => x.EndTime <= request.To)
                .ToList();

            var teacherPaySlips = eventsInRange.SelectMany(x => x.GetPaySlips());

            var teacherInvoiceLines = teacherPaySlips.GroupBy(x => x.Teacher)
                .Select(x => new TeacherInvoiceLine
                {
                    Name = x.Key.User.FullName,
                    AmountOwed = x.Sum(y => y.Pay)
                })
                .ToList();

            var report = new TeacherInvoiceResponse(teacherInvoiceLines);

            return report;
        }
    }
}