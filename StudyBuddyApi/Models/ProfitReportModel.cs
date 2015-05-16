using System.Collections.Generic;
using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class ProfitReportModel
    {
        public IList<PassProfitReport> PassProfitReports { get; set; }
        public IList<BlockProfitReport> BlockProfitReports { get; set; } 

        public ProfitReportModel Populate(IList<Pass> passesBought, List<Block> blocksWithInPeriod, IList<Pass> unlimitedPasses)
        {
            var model = new ProfitReportModel
            {
                PassProfitReports = passesBought.GroupBy(x => x.Description).Select(x => new PassProfitReport
                {
                    PassDescription = x.Key,
                    TotalRevenue = x.Sum(p => p.Cost),
                    TotalSold = x.Count()
                }).ToList(),
                BlockProfitReports = blocksWithInPeriod.Select(x => new BlockProfitReport(x.Classes.Select(c => new ClassProfitReport
                {
                    TotalNumberOfTeachers = c.Teachers.Count,
                    TotalAttendance = c.ActualStudents.Count,
                    Revenue = c.PassStatistics.Where(ps => ps.Pass.PassType != PassType.Unlimited.ToString())
                        .Sum(ps => ps.CostPerClass) + 
                        unlimitedPasses.Where(p => p.StartDate <= c.StartTime && p.EndDate >= c.StartTime)
                        .Sum(p => p.PassStatistic.CostPerClass)
                }).ToList())).ToList()
            };

            return model;

        }
    }

    public class PassProfitReport
    {
        public string PassDescription { get; set; }
        public int TotalSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class BlockProfitReport
    {
        public BlockProfitReport(IList<ClassProfitReport> classProfitReports)
        {
            ClassProfitReports = classProfitReports;
        }

        public IList<ClassProfitReport> ClassProfitReports { get; set; }

        public int TotalAttendance
        {
            get { return ClassProfitReports.Sum(x => x.TotalAttendance); }
        }

        public int TotalNumberOfTeachers
        {
            get { return ClassProfitReports.Sum(x => x.TotalNumberOfTeachers); }
        }

        public decimal TotalRevenue
        {
            get { return ClassProfitReports.Sum(x => x.Revenue); }
        }
    }

    public class ClassProfitReport
    {
        public int TotalAttendance { get; set; }
        public int TotalNumberOfTeachers { get; set; }
        public decimal Revenue { get; set; }
    }
}