using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        List<UserDTO> Get(int pageNumber, int pageQuantity);
        IEnumerable<Book> GetBooksByUser(int userId);
        User? GetById(int id);
        User Authenticate(string email, string password);
        void Update(User user);
        void IncrementTotalReadingTime(int userId, int timeToAdd);
        void Delete(int id);
    }
}
