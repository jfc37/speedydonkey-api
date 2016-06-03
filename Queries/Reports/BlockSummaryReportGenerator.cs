using System.Linq;
using Common.Extensions;
using Contracts.Reports.BlockSummary;
using Contracts.Reports.TeacherInvoices;
using Core.Queries.Reports;
using Data.Repositories;
using Models;
using NHibernate.Linq;

namespace Queries.Reports
{
    /// <summary>
    /// Generates block summary reports
    /// </summary>
    public class BlockSummaryReportGenerator : IBlockSummaryReportGenerator
    {
        private readonly IRepository<Block> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockSummaryReportGenerator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BlockSummaryReportGenerator(IRepository<Block> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public BlockSummaryResponse Create(DateRangeReportRequest request)
        {
            request.GuardAgainstNull(nameof(request));

            var blah = _repository.Queryable()
                //.FetchMany(x => x.Classes)
                //.ThenFetchMany(x => x.ActualStudents)
                .Where(x => x.StartDate >= request.From)
                .Where(x => x.EndDate <= request.To)
                .ToList();

            var blockSummaries = _repository.Queryable()
                //.FetchMany(x => x.Classes)
                //.ThenFetchMany(x => x.ActualStudents)
                .Where(x => x.StartDate >= request.From)
                .Where(x => x.EndDate <= request.To)
                .ToList()
                .Select(x => new BlockDetailLine
                {
                    Name = x.Name,
                    BlockId = x.Id,
                    Attendance = x.Classes.Sum(y => y.ActualStudents.Count),
                    Revenue = x.Classes.Sum(y => y.PassStatistics.Sum(z => z.CostPerClass))
                })
                .ToList();
            
            var report = new BlockSummaryResponse(blockSummaries);

            return report;
        }
    }
}