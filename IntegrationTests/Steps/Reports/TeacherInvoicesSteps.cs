using System;
using Common.Extensions.DateTimes;
using Contracts.Reports.TeacherInvoices;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Reports
{
    [Binding]
    public class TeacherInvoicesSteps
    {
        [When(@"the teacher invoice report is requested with no dates")]
        public void WhenTheTeacherInvoiceReportIsRequestedWithNoDates()
        {
            RequestReport("reports/teacher-invoices");
        }

        [When(@"the teacher invoice report is requested with from date being after to date")]
        public void WhenTheTeacherInvoiceReportIsRequestedWithFromDateBeingAfterToDate()
        {
            var from = DateTime.Today;
            var to = from.AddDays(-1);

            var url = Routes.GetTeacherInvoiceReport(from, to);
            RequestReport(url);
        }


        [When(@"the teacher invoice report is requested")]
        public void WhenTheTeacherInvoiceReportIsRequested()
        {
            var from = DateTime.Today;
            var to = from.AddWeeks(4);

            var url = Routes.GetTeacherInvoiceReport(from, to);
            RequestReport(url);
        }

        private static void RequestReport(string url)
        {
            var response = ApiCaller.Get<TeacherInvoiceResponse>(url);

            ScenarioCache.StoreResponse(response);
            ScenarioCache.Store(ModelKeys.TeacherInvoiceReport, response.Data);
        }

        [Then(@"the teacher invoice report has '(.*)' teacher")]
        public void ThenTheTeacherInvoiceReportHasTeacher(int expectedNumberOfTeachers)
        {
            var report = ScenarioCache.Get<TeacherInvoiceResponse>(ModelKeys.TeacherInvoiceReport);

            Assert.AreEqual(expectedNumberOfTeachers, report.Lines.Count);
        }

        [Then(@"the teacher invoice report totals '(.*)'")]
        public void ThenTheTeacherInvoiceReportTotals(int expectedTotalOwed)
        {
            var report = ScenarioCache.Get<TeacherInvoiceResponse>(ModelKeys.TeacherInvoiceReport);

            Assert.AreEqual(expectedTotalOwed, report.TotalOwed);
        }

    }
}
