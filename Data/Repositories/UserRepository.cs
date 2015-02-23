using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int userId);

        User Create(User user);

        User Update(User user);

        void Delete(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ISpeedyDonkeyDbContext _context;

        public UserRepository(ISpeedyDonkeyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(x => x.Person)
                .ToList();
        }

        public User Get(int userId)
        {
            return _context.Users
                .Include(x => x.Person)
                .FirstOrDefault(x => x.Id == userId);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _context.SaveChanges();
            return user;
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
