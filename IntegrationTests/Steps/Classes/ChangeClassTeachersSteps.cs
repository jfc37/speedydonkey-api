﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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
            ScenarioCache.Store(ModelIdKeys.ClassKeyId, classModel.Id);
            ScenarioCache.Store(OriginalTeacherIdsKey, currentClassTeacherIds);
            ScenarioCache.Store(NewTeacherIdsKey, newClassTeacherIds);
        }

        [When(@"the class teachers are changed")]
        public void WhenTheClassTeachersAreChanged()
        {
            var classId = ScenarioCache.GetId(ModelIdKeys.ClassKeyId);

            var response = ApiCaller.Put<ActionReponse<ClassModel>>(ScenarioCache.Get<List<int>>(NewTeacherIdsKey),
                Routes.GetChangeClassTeachers(ScenarioCache.GetId(ModelIdKeys.ClassKeyId)));

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the class teachers are updated")]
        public void ThenTheClassTeachersAreUpdated()
        {
            var classModel = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, ScenarioCache.GetId(ModelIdKeys.ClassKeyId))).Data;

            var expectedNewTeachers = ScenarioCache.Get<List<int>>(NewTeacherIdsKey);
            var unexpectedOriginalTeachers = ScenarioCache.Get<List<int>>(OriginalTeacherIdsKey);

            var currentClassTeachers = classModel.Teachers.Select(x => x.Id).ToList();
            Assert.AreEqual(expectedNewTeachers, currentClassTeachers);
            Assert.AreNotEqual(unexpectedOriginalTeachers, currentClassTeachers);
        }
    }
}
