using MyReading.API.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<Book> Get(int pageNumber, int pageQuantity);
        Book? GetById(int id);
        void Update(Book book);
        void Delete(int id);
    }
}
