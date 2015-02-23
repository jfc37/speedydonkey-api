using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockLectureRepositoryBuilder : MockBuilder<ILectureRepository>
    {
        private readonly IList<Lecture> _lectures;

        public MockLectureRepositoryBuilder()
        {
            _lectures = new List<Lecture>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Lecture) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(_lectures);
        }

        public MockLectureRepositoryBuilder WithSomeValidLecture()
        {
            _lectures.Add(new Lecture());
            return this;
        }

        public MockLectureRepositoryBuilder WithNoLectures()
        {
            _lectures.Clear();
            return this;
        }

        public MockLectureRepositoryBuilder WithLecture(Lecture lecture)
        {
            _lectures.Add(lecture);

            Mock.Setup(x => x.Get(lecture.Id))
                .Returns(lecture);

            return this;
        }

        public MockLectureRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Lecture>()))
                .Returns<Lecture>(x => new Lecture { Id = 543 });

            return this;
        }

        public MockLectureRepositoryBuilder WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<Lecture>()));
            return this;
        }

        public MockLectureRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<Lecture>()))
                .Returns<Lecture>(x => x);

            return this;
        }
    }
}