using Microsoft.AspNetCore.Mvc;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Model;
using MyReading.API.ViewModel;

namespace MyReading.API.Controllers
{
    [ApiController]
    [Route("api/v1/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult Add(BookViewModel bookView)
        {
            var book = new Book(bookView.Id, bookView.Title, bookView.Author, bookView.Pages, bookView.DateRead);
            _bookRepository.Add(book);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookViewModel bookView)
        {
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = bookView.Title;
            existingBook.Author = bookView.Author;
            existingBook.Pages = bookView.Pages;
            existingBook.DateRead = bookView.DateRead;

            _bookRepository.Update(existingBook);
            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(id);
            return Ok();
        }
    }
}
