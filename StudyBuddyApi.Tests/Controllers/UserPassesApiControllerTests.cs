using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
    public class UserPassesApiControllerTests
    {
        protected MockUrlConstructorBuilder UrlConstructorBuilder;
        protected MockRepositoryBuilder<User> RepositoryBuilder;
        protected ICommonInterfaceCloner Cloner;

        protected UserPassesApiController GetController()
        {
            var controller = new UserPassesApiController(
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
            RepositoryBuilder = new MockRepositoryBuilder<User>();
            Cloner = new CommonInterfaceCloner();
        }

        public class GivenAGetIsMade : UserPassesApiControllerTests
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

            public class WhenUserDoesntHaveAnyPasses : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new User());
                }

                [Test]
                public void Then_response_should_be_not_found()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }

            public class WhenUserDoesntHaveAnyValidPasses : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new User
                    {
                        Passes = new List<IPass>
                        {
                            new Pass()
                        }
                    });
                }

                [Test]
                public void Then_response_should_be_not_found()
                {
                    var response = PerformAction();

                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }

            public class WhenUserHasAValidPasses : GivenAGetIsMade
            {
                [SetUp]
                public override void Setup()
                {
                    base.Setup();
                    RepositoryBuilder.WithGet(new User
                    {
                        Passes = new List<IPass>
                        {
                            new ClipPass
                            {
                                ClipsRemaining = 3,
                                StartDate = DateTime.Now.AddDays(-3),
                                EndDate = DateTime.Now.AddDays(3)
                            }
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
