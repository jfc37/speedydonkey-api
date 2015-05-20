using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Action;
using ActionHandlers.UpdateHandlers;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateBlockHandlerTests
    {
        private MockRepositoryBuilder<Block> _repositoryBuilder;
        private MockRepositoryBuilder<Teacher> _teacherRepositoryBuilder;
        private UpdateBlock _action;
        private Block _existingBlock;

        private void PerformAction()
        {
            new UpdateBlockHandler(_repositoryBuilder.BuildObject(), _teacherRepositoryBuilder.BuildObject()).Handle(_action);
        }

        [SetUp]
        public void Setup()
        {
            _existingBlock = new Block
            {
                Name = "old",
                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MaxValue,
                Teachers = new List<ITeacher> { new Teacher{ Id = 1 } },
                Classes = new Collection<IClass>()
            };
            _action = new UpdateBlock(new Block
            {
                Name = "new",
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
                Teachers = new List<ITeacher> { new Teacher { Id = 1 } }
            });
            _repositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithGet(_existingBlock)
                .WithUpdate();
            _teacherRepositoryBuilder = new MockRepositoryBuilder<Teacher>()
                .WithSuccessfulGet();
        }

        [Test]
        public void It_should_update_name()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Block>()));
            Assert.AreEqual("new", _repositoryBuilder.UpdatedEntity.Name);
        }

        public class WhenThereHasBeenNoChangeInTeachers : UpdateBlockHandlerTests
        {
            [Test]
            public void Then_teachers_arent_retrieved()
            {
                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
            }
        }

        public class WhenAnotherTeacherHasBeenAdded : UpdateBlockHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Block();
                _action.ActionAgainst.Teachers = new List<ITeacher>(_existingBlock.Teachers);
                _action.ActionAgainst.Teachers.Add(new Teacher{ Id = 2 });

                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenRemoved : UpdateBlockHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Block
                {
                    Teachers = new List<ITeacher>
                    {
                        new Teacher{Id = 1}
                    }
                };
                _existingBlock.Teachers.Add(new Teacher { Id = 2 });

                PerformAction();

                _teacherRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenChanged : UpdateBlockHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Block
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
