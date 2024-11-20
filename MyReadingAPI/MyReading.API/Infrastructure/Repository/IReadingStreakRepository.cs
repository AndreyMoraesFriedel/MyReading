using MyReading.API.Domain.DTOs;
using MyReading.API.Domain.Model;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IReadingStreakRepository
    {
        void Add(ReadingStreak streak);
        List<ReadingStreakDTO> Get(int pageNumber, int pageQuantity);
        ReadingStreak? GetById(int id);
        List<ReadingStreak> GetByUserId(int userId);
        void Update(ReadingStreak streak);
        void Delete(int id);
    }
}