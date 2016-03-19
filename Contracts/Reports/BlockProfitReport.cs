using System.Collections.Generic;
using System.Linq;

namespace Contracts.Reports
{
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
}