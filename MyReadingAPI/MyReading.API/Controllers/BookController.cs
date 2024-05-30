using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyReading.API.Infrastructure.Repository;
using MyReading.API.Domain.Model;
using MyReading.API.Application.ViewModel;
using AutoMapper;
using MyReading.API.Domain.DTOs;

namespace MyReading.API.Controllers
{
    [ApiController]
    [Route("api/v1/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] BookViewModel bookView)
        {
            var filePath = Path.Combine("Storage", bookView.Capa.FileName);

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                bookView.Capa.CopyTo(fileStream);
            }

            var book = new Book(bookView.Id, bookView.Title, bookView.Author, filePath, bookView.Pages, bookView.DateRead);
            _bookRepository.Add(book);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}/download")]
        public IActionResult DownloadCapa(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            var dataBytes = System.IO.File.ReadAllBytes(book.Capa);
            return File(dataBytes, "image/png");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            var books = _bookRepository.Get(pageNumber, pageQuantity);
            var booksDTO = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(booksDTO);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = _mapper.Map<BookDTO>(book);
            return Ok(bookDTO);
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

                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    bookView.Capa.CopyTo(fileStream);
                }
                existingBook.Capa = filePath;
            }

            _bookRepository.Update(existingBook);
            var updatedBookDTO = _mapper.Map<BookDTO>(existingBook);
            return Ok(updatedBookDTO);
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
