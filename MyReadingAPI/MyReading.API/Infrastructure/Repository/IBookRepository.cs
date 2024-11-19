using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<BookDTO> Get(int pageNumber, int pageQuantity);
        Book? GetById(int id);
        void Update(Book book);
        void Delete(int id);
    }
}
