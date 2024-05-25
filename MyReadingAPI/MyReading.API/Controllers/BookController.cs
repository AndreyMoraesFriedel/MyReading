using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] BookViewModel bookView)
        {
            var filePath = Path.Combine("Storage", bookView.Capa.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            bookView.Capa.CopyTo(fileStream);

            var book = new Book(bookView.Id, bookView.Title, bookView.Author, filePath, bookView.Pages, bookView.DateRead);
            _bookRepository.Add(book);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/download")]
        public IActionResult DownloadCapa(int id)
        { 
            var book = _bookRepository.GetById(id);
            var dataBytes = System.IO.File.ReadAllBytes(book.Capa);

            return File(dataBytes, "image/png");
        }

        [Authorize]
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

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] BookViewModel bookView)
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

            if (bookView.Capa != null)
            {
                var filePath = Path.Combine("Storage", bookView.Capa.FileName);

                // Delete old file if exists
                if (System.IO.File.Exists(existingBook.Capa))
                {
                    System.IO.File.Delete(existingBook.Capa);
                }

                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                bookView.Capa.CopyTo(fileStream);
                existingBook.Capa = filePath;
            }

            _bookRepository.Update(existingBook);
            return Ok(existingBook);
        }

        [Authorize]
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
