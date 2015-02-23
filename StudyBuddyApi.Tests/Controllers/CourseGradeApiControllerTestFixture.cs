using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Data.Tests.Builders.MockBuilders;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests.Controllers
{
    [TestFixture]
    public class CourseGradeApiControllerTestFixture
    {
        private CourseGradeApiControllerBuilder _controllerBuilder;
        private MockCourseGradeRepositoryBuilder _courseGradeRepositoryBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new CourseGradeApiControllerBuilder();
            _courseGradeRepositoryBuilder = new MockCourseGradeRepositoryBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
        }

        private CourseGradeApiController GetController()
        {
            var controller = _controllerBuilder
                .WithCourseGradeRepository(_courseGradeRepositoryBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : CourseGradeApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseGradeRepositoryBuilder = new MockCourseGradeRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courseGrades_exists()
            {
                _courseGradeRepositoryBuilder.WithNoCourseGrades();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_courseGrades_exist()
            {
                _courseGradeRepositoryBuilder
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_courseGrades_when_courseGrades_exist()
            {
                _courseGradeRepositoryBuilder
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade()
                    .WithSomeValidCourseGrade();

                var controller = GetController();
                var response = controller.Get(_courseId);

                IEnumerable<CourseGradeModel> courseGradesInResponse;
                Assert.IsTrue(response.TryGetContentValue(out courseGradesInResponse));
                Assert.AreEqual(4, courseGradesInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courseGrades_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int courseGradeId = 5;
                _courseGradeRepositoryBuilder.WithCourseGrade(new CourseGrade{Id = 1});

                var controller = GetController();
                var response = controller.Get(_courseId, courseGradeId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_courseGrade_exists()
            {
                const int courseGradeId = 5;
                _courseGradeRepositoryBuilder.WithCourseGrade(new CourseGrade{Id = courseGradeId});

                var controller = GetController();
                var response = controller.Get(courseGradeId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_courseGrade_with_matching_id()
            {
                const int courseGradeId = 5;
                _courseGradeRepositoryBuilder.WithCourseGrade(new CourseGrade{Id = courseGradeId});

                var controller = GetController();
                var response = controller.Get(_courseId, courseGradeId);

                CourseGradeModel courseGradeInResponse;
                Assert.IsTrue(response.TryGetContentValue(out courseGradeInResponse));
                Assert.AreEqual(courseGradeId, courseGradeInResponse.Id);
            }
        }
    }
}
