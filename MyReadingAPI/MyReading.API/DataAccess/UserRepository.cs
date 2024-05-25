using MyReading.API.Infrastructure;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Model;

namespace MyReading.API.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly MyReadingContext _context = new MyReadingContext();

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
