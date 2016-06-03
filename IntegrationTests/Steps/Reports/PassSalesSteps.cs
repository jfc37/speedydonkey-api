using System;
using System.Collections.Generic;
using Common.Extensions;
using Common.Extensions.DateTimes;
using Contracts.Passes;
using Contracts.Reports.BlockSummary;
using Contracts.Reports.PassSales;
using IntegrationTests.Steps.Passes;
using IntegrationTests.Steps.PassTemplates;
using IntegrationTests.Steps.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Reports
{
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

    [Binding]
    public class PassSalesSteps
    {
        [Given(@"'(.*)' type of pass costing '(.*)' has been sold")]
        public void GivenTypeOfPassCostingHasBeenSold(int numberSold, decimal cost)
        {
            GivenStudentHasClassPassCosting(numberSold, 6, cost);
        }

        [Given(@"'(.*)' student has '(.*)' class pass costing '(.*)'")]
        public void GivenStudentHasClassPassCosting(int numberOfStudents, int numberOfClips, decimal cost)
        {
            var createPassTemplateSteps = new CreatePassTemplateSteps();
            createPassTemplateSteps.GivenAValidPassTemplateIsReadyToBeSubmitted();

            var passTemplate = ScenarioCache.Get<PassTemplateModel>(ModelKeys.PassTemplate);
            passTemplate.ClassesValidFor = numberOfClips;
            passTemplate.Cost = cost;
            passTemplate.Description = $"Pass - {Guid.NewGuid()}";
            ScenarioCache.Store(ModelKeys.PassTemplate, passTemplate);

            createPassTemplateSteps.WhenThePassTemplateIsAttemptedToBeCreated();
            createPassTemplateSteps.ThenPassTemplateCanBeRetrieved();

            numberOfStudents.ToNumberRange().Each(x => BuyPass());
        }

        private static void BuyPass()
        {
            new CommonUserSteps().GivenAUserExists();

            var userId = ScenarioCache.GetUserId();
            var studentIds = ScenarioCache.Get<List<int>>(ModelIdKeys.StudentIds);
            studentIds.Add(userId);
            ScenarioCache.Store(ModelIdKeys.StudentIds, studentIds);

            var purchasePassSteps = new PurchasePassSteps();
            purchasePassSteps.WhenTheUserPurchasesAPassFromATeacher();
            purchasePassSteps.ThenTheUserHasAClipPass();
            purchasePassSteps.ThenThePassIsPaid();
            purchasePassSteps.ThenThePassIsValid();
        }

        [When(@"the pass sales report is requested")]
        public void WhenThePassSalesReportIsRequested()
        {
            var from = DateTime.Today;
            var to = from.AddWeeks(4);

            var url = Routes.GetPassSalesReport(from, to);
            RequestReport(url);
        }

        private static void RequestReport(string url)
        {
            var response = ApiCaller.Get<PassSalesResponse>(url);

            ScenarioCache.StoreResponse(response);
            ScenarioCache.Store(ModelKeys.PassSalesReport, response.Data);
        }

        [Then(@"the pass sales report has '(.*)' line")]
        public void ThenThePassSalesReportHasLine(int expectedNumberOfLines)
        {
            var report = ScenarioCache.Get<PassSalesResponse>(ModelKeys.PassSalesReport);

            Assert.AreEqual(expectedNumberOfLines, report.Lines.Count);
        }

        [Then(@"the pass sales report total sold is '(.*)'")]
        public void ThenThePassSalesReportTotalSoldIs(int expectedTotalPasses)
        {
            var report = ScenarioCache.Get<PassSalesResponse>(ModelKeys.PassSalesReport);

            Assert.AreEqual(expectedTotalPasses, report.TotalSold);
        }

        [Then(@"the pass sales report toal revenue is '(.*)'")]
        public void ThenThePassSalesReportToalRevenueIs(decimal expectedTotalRevenue)
        {
            var report = ScenarioCache.Get<PassSalesResponse>(ModelKeys.PassSalesReport);

            Assert.AreEqual(expectedTotalRevenue, report.TotalRevenue);
        }

        [When(@"the pass sales report is requested with no dates")]
        public void WhenThePassSalesReportIsRequestedWithNoDates()
        {
            RequestReport("reports/teacher-invoices");
        }

        [When(@"the pass sales report is requested with from date being after to date")]
        public void WhenThePassSalesReportIsRequestedWithFromDateBeingAfterToDate()
        {
            var from = DateTime.Today;
            var to = from.AddDays(-1);

            var url = Routes.GetTeacherInvoiceReport(from, to);
            RequestReport(url);
        }

    }
}