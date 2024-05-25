using MyReading.API.Infrastructure;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Model;

namespace MyReading.API.DataAccess
{
    public class BookRepository : IBookRepository
    {
        private readonly MyReadingContext _context = new MyReadingContext();

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<Book> Get(int pageNumber, int pageQuantity)
        {
            return _context.Books.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
