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
        private MockRepositoryBuilder<Teacher> _teacherRepositoryBuilder;
        private UpdateLevel _action;
        private Level _savedLevel;

        private void PerformAction()
        {
            new UpdateLevelHandler(_levelRepositoryBuilder.BuildObject(), _teacherRepositoryBuilder.BuildObject())
                .Handle(_action);
        }

        [SetUp]
        public virtual void Setup()
        {
            _savedLevel = new Level
            {
                Teachers = new List<ITeacher> { new Teacher() }
            };
            _levelRepositoryBuilder = new MockRepositoryBuilder<Level>()
                .WithGet(_savedLevel)
                .WithUpdate();
            _teacherRepositoryBuilder = new MockRepositoryBuilder<Teacher>()
                .WithSuccessfulGet();
            new CommonInterfaceCloner();
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

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
            }
        }

        public class WhenAnotherTeacherHasBeenAdded : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level();
                _action.ActionAgainst.Teachers = new List<ITeacher>(_savedLevel.Teachers);
                _action.ActionAgainst.Teachers.Add(new Teacher{Id = 2});

                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenRemoved : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level
                {
                    Teachers = new List<ITeacher>
                    {
                        new Teacher{Id = 1}
                    }
                };
                _savedLevel.Teachers.Add(new Teacher{ Id = 2 });

                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenChanged : GivenUpdateLevelHandlerIsCalled
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Level
                {
                    Teachers = new List<ITeacher>
                    {
                        new Teacher{Id = 2}
                    }
                };

                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }
    }
}
