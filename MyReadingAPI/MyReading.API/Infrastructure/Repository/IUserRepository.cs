using MyReading.API.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        List<User> Get(int pageNumber, int pageQuantity);
        User? GetById(int id);
        void Update(User user);
        void Delete(int id);
    }
}
