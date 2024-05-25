using MyReading.API.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        List<User> GetAll();
        User? GetById(int id);
        void Update(User user);
        void Delete(int id);
    }
}
