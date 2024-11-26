using MyReading.API.Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyReading.API.Domain.DTOs;

namespace MyReading.API.Infrastructure.Repository
{
    public interface IReadingProgressRepository
    {
        void Add(ReadingProgress progress);
        List<ReadingProgressDTO> Get(int pageNumber, int pageQuantity);
        ReadingProgress? GetById(int id);
        void Update(ReadingProgress progress);
        void IncrementReadingTime(int userId, int bookId, TimeSpan timeToAdd);
        void Delete(int id);
    }
}