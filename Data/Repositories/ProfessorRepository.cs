using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public class ProfessorRepository : PersonRepository<Professor>
    {
        public ProfessorRepository(ISpeedyDonkeyDbContext context) : base(context)
        {
        }

        public override IEnumerable<Professor> GetAll()
        {
            return Context.People.OfType<Professor>()
                .Include(x => x.Courses)
                .ToList();
        }

        public override Professor Get(int personId)
        {
            return Context.People.OfType<Professor>()
                .Include(x => x.Courses)
                .ToList()
                .FirstOrDefault(x => x.Id == personId);
        }
    }
}