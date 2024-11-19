using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;
using MyReading.API.Infrastructure;
using MyReading.API.Infrastructure.Repository;

namespace MyReading.API.DataAccess
{
    public class ReadingProgressRepository : IReadingProgressRepository
    {
        private readonly MyReadingContext _context = new MyReadingContext();

        public void Add(ReadingProgress readingProgress)
        {
            _context.ReadingProgresses.Add(readingProgress);
            _context.SaveChanges();
        }

        public List<ReadingProgressDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.ReadingProgresses.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(rp =>
                new ReadingProgressDTO()
                {
                    Id = rp.Id,
                    UserId = rp.UserId,
                    BookId = rp.BookId,
                    PagesRead = rp.PagesRead,
                    StartDate = rp.StartDate,
                    EndDate = rp.EndDate,
                }).ToList();
        }

        public ReadingProgress? GetById(int id)
        {
            return _context.ReadingProgresses.Find(id);
        }

        public void Update(ReadingProgress readingProgress)
        {
            _context.ReadingProgresses.Update(readingProgress);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var readingProgress = _context.ReadingProgresses.Find(id);
            if (readingProgress != null)
            {
                _context.ReadingProgresses.Remove(readingProgress);
                _context.SaveChanges();
            }
        }
    }
}
