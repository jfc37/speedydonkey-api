using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using ActionHandlersTests.Builders.MockBuilders;
using Common;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public class ClassAttendanceApiControllerTests
    {
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<Class> RepositoryBuilder;
        protected ICommonInterfaceCloner Cloner;
        protected MockActionHandlerOverlordBuilder ActionHandlerOverlordBuilder;

        protected ClassAttendanceApiController GetController()
        {
            var controller = new ClassAttendanceApiController(
                RepositoryBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                Cloner,
                ActionHandlerOverlordBuilder.BuildObject());
            ApiControllerSetup.Setup(controller);
            return controller;
        }

        [SetUp]
        public virtual void Setup()
        {
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockRepositoryBuilder<Class>();
            Cloner = new CommonInterfaceCloner();
            ActionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
        }

        public class GivenAGetIsMade : ClassAttendanceApiControllerTests
        {
            protected int Id;

            protected HttpResponseMessage PerformAction()
            {
                return GetController().Get(Id);
            }
            [SetUp]
            public override void Setup()
            {
                base.Setup();
                RepositoryBuilder.WithSuccessfulGet();
            }

            [Test]
            public void Then_it_should_call_the_repository()
            {
                Id = 43;

                PerformAction();

                RepositoryBuilder.Mock.Verify(x => x.Get(Id), Times.Once);
            }

            public class WhenClassDoesNotExist : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithUnsuccessfulGet();
                }

                [Test]
                public void Then_response_should_be_not_found()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }

            public class WhenClassDoesntHaveAnyRegisteredStudents : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new Class());
                }

                [Test]
                public void Then_response_should_be_not_found()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }

            public class WhenClassHasRegisteredStudents : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new Class
                    {
                        ActualStudents = new List<IUser>
                        {
                            new User()
                        }
                    });
                }

                [Test]
                public void Then_response_should_be_ok()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}
