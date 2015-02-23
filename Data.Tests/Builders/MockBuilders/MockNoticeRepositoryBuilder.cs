using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockNoticeRepositoryBuilder : MockBuilder<INoticeRepository>
    {
        private readonly IList<Notice> _notices;

        public MockNoticeRepositoryBuilder()
        {
            _notices = new List<Notice>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Notice) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(_notices);
        }

        public MockNoticeRepositoryBuilder WithSomeValidNotice()
        {
            _notices.Add(new Notice());
            return this;
        }

        public MockNoticeRepositoryBuilder WithNoNotices()
        {
            _notices.Clear();
            return this;
        }

        public MockNoticeRepositoryBuilder WithNotice(Notice notice)
        {
            _notices.Add(notice);

            Mock.Setup(x => x.Get(notice.Id))
                .Returns(notice);

            return this;
        }

        public MockNoticeRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Notice>()))
                .Returns<Notice>(x => new Notice { Id = 543 });

            return this;
        }

        public MockNoticeRepositoryBuilder WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<Notice>()));
            return this;
        }

        public MockNoticeRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<Notice>()))
                .Returns<Notice>(x => x);

            return this;
        }
    }
}