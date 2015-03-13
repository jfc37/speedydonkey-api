using System.Collections.Generic;
using Models;
using NHibernate;

namespace Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }
    }

    public class LevelRepository : GenericRepository<Level>
    {
        public LevelRepository(ISession session)
            : base(session)
        {
        }
    }

    public class BlockRepository : GenericRepository<Block>
    {
        public BlockRepository(ISession session)
            : base(session)
        {
        }
    }

    public class PassRepository : GenericRepository<Pass>
    {
        public PassRepository(ISession session)
            : base(session)
        {
        }
    }

    public class ClassRepository : GenericRepository<Class>
    {
        public ClassRepository(ISession session)
            : base(session)
        {
        }
    }

    public class BookingRepository : GenericRepository<Booking>
    {
        public BookingRepository(ISession session)
            : base(session)
        {
        }
    }

    public class ReferenceDataRepository : GenericRepository<ReferenceData>
    {
        public ReferenceDataRepository(ISession session)
            : base(session)
        {
        }
    }
}
