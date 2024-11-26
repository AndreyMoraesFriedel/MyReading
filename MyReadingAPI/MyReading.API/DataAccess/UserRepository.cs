using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;
using MyReading.API.Infrastructure;
using MyReading.API.Infrastructure.Repository;

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

        public List<UserDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.Users.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(u =>
                new UserDTO()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Photo = u.Photo,
                    TotalReadingTime = u.TotalReadingTime,
                }).ToList();
        }
        public IEnumerable<Book> GetBooksByUser(int userId)
        {
            return _context.ReadingProgresses
                .Where(rp => rp.UserId == userId)
                .Select(rp => rp.Book) 
                .ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void IncrementTotalReadingTime(int userId, TimeSpan timeToAdd)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                user.TotalReadingTime = user.TotalReadingTime.Add(timeToAdd); 
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
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
