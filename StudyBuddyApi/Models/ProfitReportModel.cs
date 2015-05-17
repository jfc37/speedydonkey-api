using System.Collections.Generic;
using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class ProfitReportModel
    {
        public IList<PassProfitReport> PassProfitReports { get; set; }

        public int TotalPassesSold
        {
            get { return PassProfitReports.Sum(x => x.TotalSold); }
        }

        public decimal TotalPassRevenue
        {
            get { return PassProfitReports.Sum(x => x.TotalRevenue); }
        }

        public IList<BlockProfitReport> BlockProfitReports { get; set; }

        public int TotalBlockAttendance
        {
            get { return BlockProfitReports.Sum(x => x.TotalAttendance); }
        }

        public decimal TotalBlockRevenue
        {
            get { return BlockProfitReports.Sum(x => x.TotalRevenue); }
        }

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
                    Name = c.Name,
                    TotalNumberOfTeachers = c.Teachers.Count,
                    TotalAttendance = c.ActualStudents.Count,
                    Revenue = c.PassStatistics.Where(ps => ps.Pass.PassType != PassType.Unlimited.ToString())
                        .Sum(ps => ps.CostPerClass) + 
                        unlimitedPasses.Where(p => p.StartDate <= c.StartTime && p.EndDate >= c.StartTime)
                        .Sum(p => p.PassStatistic.CostPerClass)
                }).ToList())
                {
                    Name = x.Name
                }).ToList()
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

        public string Name { get; set; }

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
        public string Name { get; set; }
        public int TotalAttendance { get; set; }
        public int TotalNumberOfTeachers { get; set; }
        public decimal Revenue { get; set; }
    }
}