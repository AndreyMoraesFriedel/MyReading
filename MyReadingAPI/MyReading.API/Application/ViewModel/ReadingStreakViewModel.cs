using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Application.ViewModel
{
    public class ReadingStreakViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int LengthInDays { get; set; }
    }
}
