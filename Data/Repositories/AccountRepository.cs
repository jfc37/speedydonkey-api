using System.Collections.Generic;
using Models;

namespace Data.Repositories
{
    public class AccountRepository : GenericRepository<Account>
    {
        public AccountRepository(ISpeedyDonkeyDbContext context) : base(context)
        {
        }
    }
}
