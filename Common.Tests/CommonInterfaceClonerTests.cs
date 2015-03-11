using System;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace Common.Tests
{
    [TestFixture]
    public abstract class CommonInterfaceClonerTests<TCloneFrom, TCloneTo> where TCloneTo : new()
    {
        protected TCloneFrom CloneFrom;
        protected CommonInterfaceCloner GetCloner()
        {
            return new CommonInterfaceCloner();
        }

        protected TCloneTo PerformAction()
        {
            return GetCloner().Clone<TCloneFrom, TCloneTo>(CloneFrom);
        }
    }

    public class GivenTwoObjectsWithACommonInterface : CommonInterfaceClonerTests<LevelModel, Level>
    {
        [SetUp]
        public void Setup()
        {
            CloneFrom = new LevelModel()
            {
                Name = "some name",
                Id = 3,
                ClassesInBlock = 6,
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(9),
                Room = new Room()
            };
        }

        [Test]
        public void Then_any_string_values_should_be_copied()
        {
            var clone = PerformAction();

            Assert.AreEqual(CloneFrom.Name, clone.Name);
        }

        [Test]
        public void Then_any_int_values_should_be_copied()
        {
            var clone = PerformAction();

            Assert.AreEqual(CloneFrom.Id, clone.Id);
            Assert.AreEqual(CloneFrom.ClassesInBlock, clone.ClassesInBlock);
        }

        [Test]
        public void Then_any_date_values_should_be_copied()
        {
            var clone = PerformAction();

            Assert.AreEqual(CloneFrom.StartTime, clone.StartTime);
            Assert.AreEqual(CloneFrom.EndTime, clone.EndTime);
        }

        [Test]
        public void Then_any_inner_classes_should_not_be_copied()
        {
            var clone = PerformAction();

            Assert.AreNotEqual(CloneFrom.Room, clone.Room);
        }
    }
}
