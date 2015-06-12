using System.Linq;
using Action;
using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.DeleteHandlers
{
    public class DeletePassTemplateHandler : DeleteEntityHandler<DeletePassTemplate, PassTemplate>
    {
        public DeletePassTemplateHandler(IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }
    public class DeleteLevelHandler : DeleteEntityHandler<DeleteLevel, Level>
    {
        public DeleteLevelHandler(IRepository<Level> repository)
            : base(repository)
        {
        }
    }
    public class DeleteBlockHandler : DeleteEntityHandler<DeleteBlock, Block>
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public DeleteBlockHandler(IRepository<Block> repository, IRepository<Class> classRepository, IRepository<Booking> bookingRepository)
            : base(repository)
        {
            _classRepository = classRepository;
            _bookingRepository = bookingRepository;
        }

        protected override void PreHandle(ICrudAction<Block> action)
        {
            var block = Repository.Get(action.ActionAgainst.Id);
            RemoveEnroledStudentsLink(block);
            DeleteClasses(block);
            Repository.Update(block);
        }

        private void DeleteClasses(Block block)
        {
            var deleteClassHandler = new DeleteClassHandler(_classRepository, _bookingRepository);
            foreach (var theClass in block.Classes.OfType<Class>())
            {
                deleteClassHandler.Handle(new DeleteClass(theClass));
            }
        }

        private static void RemoveEnroledStudentsLink(Block block)
        {
            foreach (var enroledStudent in block.EnroledStudents)
            {
                enroledStudent.EnroledBlocks.Remove(block);
            }
        }
    }
    public class DeleteClassHandler : DeleteEntityHandler<DeleteClass, Class>
    {
        private readonly IRepository<Booking> _bookingRepository;

        public DeleteClassHandler(IRepository<Class> repository, IRepository<Booking> bookingRepository)
            : base(repository)
        {
            _bookingRepository = bookingRepository;
        }

        protected override void PreHandle(ICrudAction<Class> action)
        {
            var theClass = Repository.Get(action.ActionAgainst.Id);

            var bookings = _bookingRepository.GetAll().Where(x => x.Event == theClass);
            foreach (var booking in bookings)
            {
                _bookingRepository.Delete(booking.Id);
            }
        }
    }
    public class DeletePassHandler : DeleteEntityHandler<DeletePass, Pass>
    {
        private readonly IRepository<Class> _classRepository;

        public DeletePassHandler(IRepository<Pass> repository, IRepository<Class> classRepository)
            : base(repository)
        {
            _classRepository = classRepository;
        }

        protected override void PreHandle(ICrudAction<Pass> action)
        {
            var pass = Repository.Get(action.ActionAgainst.Id);

            var classes = _classRepository.GetAll().Where(x => x.PassStatistics.Contains(pass.PassStatistic));
            foreach (var theClass in classes)
            {
                theClass.PassStatistics.Remove(pass.PassStatistic);
                _classRepository.Update(theClass);
            }
        }
    }
    public class DeleteUserHandler : DeleteEntityHandler<DeleteUser, User>
    {
        public DeleteUserHandler(IRepository<User> repository)
            : base(repository)
        {
        }
    }
    public class DeleteAnnouncementHandler : DeleteEntityHandler<DeleteAnnouncement, Announcement>
    {
        public DeleteAnnouncementHandler(IRepository<Announcement> repository)
            : base(repository)
        {
        }
    }
}
