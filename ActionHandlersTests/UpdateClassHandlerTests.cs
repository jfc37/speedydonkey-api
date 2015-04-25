using System;
using System.Collections.Generic;
using Action;
using ActionHandlers.UpdateHandlers;
using Data.Tests.Builders;
using Models;
using Moq;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateClassHandlerTests
    {
        private MockRepositoryBuilder<Class> _repositoryBuilder;
        private MockRepositoryBuilder<User> _userRepositoryBuilder;
        private UpdateClass _action;
        private Class _existingClass;

        private void PerformAction()
        {
            new UpdateClassHandler(_repositoryBuilder.BuildObject(), _userRepositoryBuilder.BuildObject()).Handle(_action);
        }

        [SetUp]
        public virtual void Setup()
        {
            _existingClass = new Class
            {
                Name = "old",
                StartTime = DateTime.MaxValue,
                EndTime = DateTime.MaxValue,
                Teachers = new List<IUser>
                {
                    new User{Id = 1}
                }
            };
            _action = new UpdateClass(new Class
            {
                Name = "new",
                StartTime = DateTime.MinValue,
                EndTime = DateTime.MinValue,
                Teachers = new List<IUser>
                {
                    new User{Id = 1}
                }
            });
            _repositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGet(_existingClass)
                .WithUpdate();
            _userRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithSuccessfulGet();
        }

        [Test]
        public void It_should_update_name()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual("new", _repositoryBuilder.UpdatedEntity.Name);
        }

        [Test]
        public void It_should_update_start_time()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual(DateTime.MinValue, _repositoryBuilder.UpdatedEntity.StartTime);
        }

        [Test]
        public void It_should_update_end_time()
        {
            PerformAction();

            _repositoryBuilder.Mock.Verify(x => x.Update(It.IsAny<Class>()));
            Assert.AreEqual(DateTime.MinValue, _repositoryBuilder.UpdatedEntity.EndTime);
        }

        public class WhenThereHasBeenNoChangeInTeachers : UpdateClassHandlerTests
        {
            [Test]
            public void Then_teachers_arent_retrieved()
            {
                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
            }
        }

        public class WhenAnotherTeacherHasBeenAdded : UpdateClassHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Class();
                _action.ActionAgainst.Teachers = new List<IUser>(_existingClass.Teachers);
                _action.ActionAgainst.Teachers.Add(new User { Id = 2 });

                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenRemoved : UpdateClassHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Class
                {
                    Teachers = new List<IUser>
                    {
                        new User {Id = 1}
                    }
                };
                _existingClass.Teachers.Add(new User { Id = 2 });

                PerformAction();

                _userRepositoryBuilder.Mock.Verify(x => x.Get(It.IsAny<int>()), Times.AtLeastOnce);
            }
        }

        public class WhenATeacherHasBeenChanged : UpdateClassHandlerTests
        {
            [Test]
            public void Then_teachers_are_retrieved()
            {
                _action.ActionAgainst = new Class
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
