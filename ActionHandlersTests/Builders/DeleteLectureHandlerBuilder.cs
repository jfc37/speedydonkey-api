using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class DeleteLectureHandlerBuilder
    {
        private ILectureRepository _lectureRepository;

        public DeleteLectureHandler Build()
        {
            return new DeleteLectureHandler(_lectureRepository);
        }

        public DeleteLectureHandlerBuilder WithLectureRepository(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            return this;
        }
    }
}