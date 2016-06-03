using System.Linq;
using Common.Extensions;
using Contracts.Reports.PassSales;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports;
using Data.Repositories;
using Models;

namespace Queries.Reports
{
    /// <summary>
    /// Generates pass sales reports
    /// </summary>
    public class PassSalesReportGenerator : IPassSalesReportGenerator
    {
        private readonly IRepository<Pass> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassSalesReportGenerator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PassSalesReportGenerator(IRepository<Pass> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PassSalesResponse Create(DateRangeReportRequest request)
        {
            request.GuardAgainstNull(nameof(request));

            var passSalesLines = _repository.Queryable()
                .Where(x => x.CreatedDateTime >= request.From)
                .Where(x => x.CreatedDateTime <= request.To)
                .GroupBy(x => x.Description)
                .Select(x => new PassSaleLine
                {
                    Name = x.Key,
                    NumberSold = x.Count(),
                    Revenue = x.Sum(y => y.Cost)
                })
                .ToList();
            
            var report = new PassSalesResponse(passSalesLines);

            return report;
        }
    }
}