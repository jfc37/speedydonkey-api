using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class NoticeRepositoryTestFixture
    {
        private NoticeRepositoryBuilder _noticeRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _noticeRepositoryBuilder = new NoticeRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private NoticeRepository BuildNoticeRepository()
        {
            return _noticeRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : NoticeRepositoryTestFixture
        {
            private int _noticeId;

            [SetUp]
            public void Setup()
            {
                _noticeId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_notice_id_isnt_found()
            {
                _contextBuilder.WithNoNotices();

                var noticeRepository = BuildNoticeRepository();
                var notice = noticeRepository.Get(_noticeId);

                Assert.IsNull(notice);
            }

            [Test]
            public void It_should_return_all_notices()
            {
                var savedNotice = new Notice {Id = _noticeId};
                _contextBuilder.WithNotice(savedNotice);

                var noticeRepository = BuildNoticeRepository();
                var notice = noticeRepository.Get(_noticeId);

                Assert.AreSame(savedNotice, notice);
            }
        }

        public class GetAll : NoticeRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
            }

            [Test]
            public void It_should_return_empty_when_no_notices_exists()
            {
                _contextBuilder.WithNoNotices();

                var noticeRepository = BuildNoticeRepository();
                var results = noticeRepository.GetAll(_courseId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_notices()
            {
                var course = new CourseBuilder()
                    .WithId(_courseId)
                    .Build();

                _contextBuilder
                    .WithNotice(new NoticeBuilder().WithCourse(course).Build())
                    .WithNotice(new NoticeBuilder().WithCourse(course).Build())
                    .WithNotice(new NoticeBuilder().WithCourse(course).Build());

                var noticeRepository = BuildNoticeRepository();
                var results = noticeRepository.GetAll(_courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().Notices.Count(), results.Count());
            }
        }

        public class Create : NoticeRepositoryTestFixture
        {
            private Notice _notice;

            [SetUp]
            public void Setup()
            {
                var course = new Course {Id = 6555};
                _notice = new NoticeBuilder()
                    .WithCourse(course)
                    .Build();
                _contextBuilder.WithNoNotices()
                    .WithCourse(course);
            }

            [Test]
            public void It_should_add_notice_to_the_database()
            {
                var noticeRepository = BuildNoticeRepository();
                noticeRepository.Create(_notice);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().Notices.ToList());
            }
        }

        public class Update : NoticeRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_notice_to_database_on_update()
            {
                Notice notice = new NoticeBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithNotice(notice);

                var repository = BuildNoticeRepository();
                repository.Update(notice);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.Notices.Count());
            }
        }

        public class Delete : NoticeRepositoryTestFixture
        {
            private Notice _notice;

            [SetUp]
            public void Setup()
            {
                _notice = new NoticeBuilder().WithId(1).Build();
                _contextBuilder.WithNotice(_notice);
            }

            [Test]
            public void It_should_delete_notice_to_the_database()
            {
                var noticeRepository = BuildNoticeRepository();
                noticeRepository.Delete(_notice);

                Assert.IsEmpty(_contextBuilder.BuildObject().Notices.ToList());
            }
        }
    }

    public class NoticeRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public NoticeRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public NoticeRepository Build()
        {
            return new NoticeRepository(_context);
        }
    }
}
