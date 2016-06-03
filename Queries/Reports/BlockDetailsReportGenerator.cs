using System.Linq;
using Common.Extensions;
using Contracts.Reports.BlockDetails;
using Core.Queries.Reports;
using Data.Repositories;
using Models;

namespace Queries.Reports
{
    /// <summary>
    /// Generates block details reports
    /// </summary>
    public class BlockDetailsReportGenerator : IBlockDetailsReportGenerator
    {
        private readonly IRepository<Block> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockSummaryReportGenerator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BlockDetailsReportGenerator(IRepository<Block> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public BlockDetailsResponse Create(BlockDetailsRequest request)
        {
            request.GuardAgainstNull(nameof(request));

            var classDetailLines = _repository.Get(request.BlockId).Classes.Select(x => new ClassDetailLine
            {
                ClassId = x.Id,
                Name = x.Name,
                Attendance = x.ActualStudents.Count,
                Revenue = x.PassStatistics.Sum(y => y.CostPerClass)
            }).ToList();

            var report = new BlockDetailsResponse(classDetailLines);

            return report;
        }
    }
}