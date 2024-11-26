namespace MyReading.API.Domain.DTOs
{
    public class ReadingProgressDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        /*public int PagesRead { get; set; }*/
        public TimeSpan TotalReadingTime { get; set; }

    }
}