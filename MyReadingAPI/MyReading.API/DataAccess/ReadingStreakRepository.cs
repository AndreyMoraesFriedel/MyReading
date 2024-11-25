using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;
using MyReading.API.Infrastructure;
using MyReading.API.Infrastructure.Repository;

namespace MyReading.API.DataAccess
{
    public class ReadingStreakRepository : IReadingStreakRepository
    {
        private readonly MyReadingContext _context = new MyReadingContext();

        public void Add(ReadingStreak readingStreak)
        {
            _context.ReadingStreaks.Add(readingStreak);
            _context.SaveChanges();
        }

        public List<ReadingStreakDTO> Get(int pageNumber, int pageQuantity)
        {
            return _context.ReadingStreaks.Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(rs =>
                new ReadingStreakDTO()
                {
                    Id = rs.Id,
                    UserId = rs.UserId,
                    LengthInDays = rs.LengthInDays,
                }).ToList();
        }

        public ReadingStreak? GetById(int id)
        {
            return _context.ReadingStreaks.Find(id);
        }
        public List<ReadingStreak> GetByUserId(int userId)
        {
            return _context.ReadingStreaks
                .Where(rs => rs.UserId == userId)
                .ToList();
        }

        public void Update(ReadingStreak readingStreak)
        {
            _context.ReadingStreaks.Update(readingStreak);
            _context.SaveChanges();
        }
        public void UpdateLengthInDays(int id, int lengthInDays)
        {
            var readingStreak = _context.ReadingStreaks.Find(id);
            if (readingStreak != null)
            {
                readingStreak.LengthInDays = lengthInDays;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var readingStreak = _context.ReadingStreaks.Find(id);
            if (readingStreak != null)
            {
                _context.ReadingStreaks.Remove(readingStreak);
                _context.SaveChanges();
            }
        }
    }
}
