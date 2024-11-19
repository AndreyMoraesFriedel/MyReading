namespace MyReading.API.Domain.DTOs
{
    public class ReadingStreakDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LengthInDays { get; set; }
    }
}