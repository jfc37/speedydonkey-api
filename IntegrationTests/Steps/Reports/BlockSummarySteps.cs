using System;
using System.Linq;
using Contracts.Reports.BlockDetails;
using Contracts.Reports.BlockSummary;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Reports
{
    [Binding]
    public class BlockDetailsSteps
    {
        [When(@"the block details report is requested")]
        public void WhenTheBlockDetailsReportIsRequested()
        {
            var blockId  = ScenarioCache.GetId(ModelIdKeys.BlockId);

            var url = Routes.GetBlockDetailsReport(blockId);
            var response = ApiCaller.Get<BlockDetailsResponse>(url);

            ScenarioCache.StoreResponse(response);
            ScenarioCache.Store(ModelKeys.BlockDetailsReport, response.Data);
        }

        [Then(@"the block details report has '(.*)' line")]
        public void ThenTheBlockDetailsReportHasLine(int expectedNumberOfLines)
        {
            var report = ScenarioCache.Get<BlockDetailsResponse>(ModelKeys.BlockDetailsReport);

            Assert.AreEqual(expectedNumberOfLines, report.Lines.Count);
        }

        [Then(@"the block details total attendance is '(.*)'")]
        public void ThenTheBlockDetailsTotalAttendanceIs(int expectedAttendance)
        {
            var report = ScenarioCache.Get<BlockDetailsResponse>(ModelKeys.BlockDetailsReport);

            Assert.AreEqual(expectedAttendance, report.TotalAttendance);
        }

        [Then(@"the block details total revenue is '(.*)'")]
        public void ThenTheBlockDetailsTotalRevenueIs(decimal expectedRevenue)
        {
            var report = ScenarioCache.Get<BlockDetailsResponse>(ModelKeys.BlockDetailsReport);

            Assert.AreEqual(expectedRevenue, report.TotalRevenue);
        }

        [Then(@"line '(.*)' of block details report revenue is '(.*)'")]
        public void ThenLineOfBlockDetailsReportRevenueIs(int lineNumber, decimal expectedRevenue)
        {
            var line = GetLine(lineNumber);

            Assert.AreEqual(expectedRevenue, line.Revenue);
        }

        [Then(@"line '(.*)' of block details report attendance is '(.*)'")]
        public void ThenLineOfBlockDetailsReportAttendanceIs(int lineNumber, int expectedAttendance)
        {
            var line = GetLine(lineNumber);

            Assert.AreEqual(expectedAttendance, line.Attendance);
        }

        private static ClassDetailLine GetLine(int lineNumber)
        {
            var report = ScenarioCache.Get<BlockDetailsResponse>(ModelKeys.BlockDetailsReport);

            var line = report.Lines.Skip(lineNumber - 1).Take(1).Single();
            return line;
        }
    }

    [Binding]
    public class BlockSummarySteps
    {
        [When(@"the block summary report is requested")]
        public void WhenTheBlockSummaryReportIsRequested()
        {
            var from = DateTime.Today;
            var to = from.AddMonths(4);

            var url = Routes.GetBlockSummaryReport(from, to);
            RequestReport(url);
        }

        private static void RequestReport(string url)
        {
            var response = ApiCaller.Get<BlockSummaryResponse>(url);

            ScenarioCache.StoreResponse(response);
            ScenarioCache.Store(ModelKeys.BlockSummaryReport, response.Data);
        }

        [Then(@"the block summary report has '(.*)' line")]
        public void ThenTheBlockSummaryReportHasLine(int expectedNumberOfLines)
        {
            var report = ScenarioCache.Get<BlockSummaryResponse>(ModelKeys.BlockSummaryReport);

            Assert.AreEqual(expectedNumberOfLines, report.Lines.Count);
        }

        [Then(@"the block summary total attendance is '(.*)'")]
        public void ThenTheBlockSummaryTotalAttendanceIs(int expectedAttendance)
        {
            var report = ScenarioCache.Get<BlockSummaryResponse>(ModelKeys.BlockSummaryReport);

            Assert.AreEqual(expectedAttendance, report.TotalAttendance);
        }

        [Then(@"the block summary total revenue is '(.*)'")]
        public void ThenTheBlockSummaryTotalRevenueIs(decimal expectedRevenue)
        {
            var report = ScenarioCache.Get<BlockSummaryResponse>(ModelKeys.BlockSummaryReport);

            Assert.AreEqual(expectedRevenue, report.TotalRevenue);
        }
    }
}