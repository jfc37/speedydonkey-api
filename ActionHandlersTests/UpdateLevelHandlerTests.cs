using System.Collections.Generic;
using Action;
using ActionHandlers.UpdateHandlers;
using Common;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class GivenUpdateLevelHandlerIsCalled
    {
        private MockRepositoryBuilder<Level> _levelRepositoryBuilder;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private ICommonInterfaceCloner _cloner;
        private UpdateLevel _action;
        private Level _savedLevel;

        private void PerformAction()
        {
            new UpdateLevelHandler(_levelRepositoryBuilder.BuildObject(), _userRepositoryBuilder.BuildObject(), _cloner)
                .Handle(_action);
        }

        [SetUp]
        public virtual void Setup()
        {
            _savedLevel = new Level
            {
                Teachers = new List<IUser>{new User() }
            };
            _levelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithGet(_savedLevel)
                .WithUpdate();
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithSuccessfulGet();
            _cloner = new CommonInterfaceCloner();
            _action = new UpdateLevel(_savedLevel);
        }

        [Test]
        public void Then_level_is_updated()
        {
            PerformAction();

            _levelRepositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Level>()), Times.Once);
        }

        public class WhenThereHasBeenNoChangeInTeachers : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_arent_retrieved()
            {
                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
            }
        }

        public class WhenAnotherTeacherHasBeenAdded : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level();
                _action.ActionAgainst.Teachers = new List<IUser>(_savedLevel.Teachers);
                _action.ActionAgainst.Teachers.Add(new User{Id = 2});

                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenRemoved : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level
                {
                    Teachers = new List<IUser>
                    {
                        new User {Id = 1}
                    }
                };
                _savedLevel.Teachers.Add(new User { Id = 2 });

                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenChanged : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level
                {
                    Teachers = new List<IUser>
                    {
                        new User {Id = 2}
                    }
                };

                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }
    }
}
