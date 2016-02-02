using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts;
using Contracts.Classes;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Classes
{
    [Binding]
    public class ChangeClassTeachersSteps
    {
        private const string OriginalTeacherIdsKey = "OriginalTeacherIdsKey";
        private const string NewTeacherIdsKey = "NewTeacherIdsKey";

        [Given(@"a class needs the teachers changed")]
        public void GivenAClassNeedsTheTeachersChanged()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, 1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var classModel = response.Data;

            var currentClassTeacherIds = classModel.Teachers.Select(x => x.Id).ToList();
            var newClassTeacherIds = new List<int> {currentClassTeacherIds.Max() + 1, currentClassTeacherIds.Max() + 2};
            ScenarioCache.Store(ModelIdKeys.Class, classModel.Id);
            ScenarioCache.Store(OriginalTeacherIdsKey, currentClassTeacherIds);
            ScenarioCache.Store(NewTeacherIdsKey, newClassTeacherIds);
        }

        [When(@"the class teachers are changed")]
        public void WhenTheClassTeachersAreChanged()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.Class);

            var response = ApiCaller.Put<ActionReponse<ClassModel>>(ScenarioCache.Get<List<int>>(NewTeacherIdsKey),
                Routes.GetChangeClassTeachers(ScenarioCache.GetId(ModelIdKeys.Class)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the class teachers are updated")]
        public void ThenTheClassTeachersAreUpdated()
        {
            var classModel = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.Class))).Data;

            var expectedNewTeachers = ScenarioCache.Get<List<int>>(NewTeacherIdsKey);
            var unexpectedOriginalTeachers = ScenarioCache.Get<List<int>>(OriginalTeacherIdsKey);

            var currentClassTeachers = classModel.Teachers.Select(x => x.Id).ToList();
            Assert.AreEqual(expectedNewTeachers, currentClassTeachers);
            Assert.AreNotEqual(unexpectedOriginalTeachers, currentClassTeachers);
        }
    }
}
