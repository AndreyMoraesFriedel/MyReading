using Microsoft.AspNetCore.Mvc;
using MyReading.API.Models;

namespace MyReading.API.Controllers
{
    // Marca esta classe como um controlador de API
    [ApiController]
    // Define a rota base para este controlador
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // Lista estática de livros para simular um banco de dados
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Book One", Author = "Author One", Pages = 300, DateRead = DateTime.Now.AddDays(-10) },
            new Book { Id = 2, Title = "Book Two", Author = "Author Two", Pages = 250, DateRead = DateTime.Now.AddDays(-5) }
        };

        // Endpoint GET para obter todos os livros
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return books;
        }

        // Endpoint GET para obter um livro por ID
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // Endpoint POST para criar um novo livro
        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }
    }
}
