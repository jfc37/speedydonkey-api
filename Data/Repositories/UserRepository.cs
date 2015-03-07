using System.Collections.Generic;
using Models;

namespace Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(ISpeedyDonkeyDbContext context)
            : base(context)
        {
        }
    }
}
