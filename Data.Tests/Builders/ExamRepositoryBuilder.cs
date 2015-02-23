using Data.Repositories;

namespace Data.Tests.Builders
{
    public class ExamRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public ExamRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public ExamRepository Build()
        {
            return new ExamRepository(_context);
        }
    }
}