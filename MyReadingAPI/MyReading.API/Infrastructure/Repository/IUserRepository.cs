using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        List<UserDTO> Get(int pageNumber, int pageQuantity);
        User? GetById(int id);
        User Authenticate(string email, string password);
        void Update(User user);
        void Delete(int id);
    }
}
