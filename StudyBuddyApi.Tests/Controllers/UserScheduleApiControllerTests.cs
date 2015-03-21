using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Common;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public class UserScheduleApiControllerTests
    {
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockAdvancedRepositoryBuilder<User, IList<IEvent>> RepositoryBuilder;
        protected ICommonInterfaceCloner Cloner;

        protected UserScheduleApiController GetController()
        {
            var controller = new UserScheduleApiController(
                RepositoryBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                Cloner);
            ApiControllerSetup.Setup(controller);
            return controller;
        }

        [SetUp]
        public virtual void Setup()
        {
            UrlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            RepositoryBuilder = new MockAdvancedRepositoryBuilder<User, IList<IEvent>>();
            Cloner = new CommonInterfaceCloner();
        }

        public class GivenAGetIsMade : UserScheduleApiControllerTests
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
                RepositoryBuilder.WithGet(new IEvent[0]);
            }

            [Test]
            public void Then_it_should_call_the_repository()
            {
                Id = 43;

                PerformAction();

                RepositoryBuilder.Mock.Verify(x => x.Get(Id), Times.Once);
            }

            public class WhenUserDoesNotExist : GivenAGetIsMade
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

            public class WhenUserDoesntHaveAnythingScheduled : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new List<IEvent>());
                }

                [Test]
                public void Then_response_should_be_not_found()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }

            public class WhenUserHasScheduledClasses : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new List<IEvent>
                    {
                        new Class()
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

    public class MockAdvancedRepositoryBuilder<TEntity, TModel> : MockBuilder<IAdvancedRepository<TEntity, TModel>> where TEntity : IEntity
    {
        public MockAdvancedRepositoryBuilder<TEntity, TModel> WithGet(TModel model)
        {
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(model);
            return this;
        }

        public MockAdvancedRepositoryBuilder<TEntity, TModel> WithUnsuccessfulGet()
        {
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(null);
            return this;
        }
    }
}
