using Data.Repositories;

namespace Data.Tests.Builders
{
    public class UserRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public UserRepository Build()
        {
            return new UserRepository(_context);
        }

        public UserRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }
    }
}