using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Application.ViewModel
{
    public class ReadingStreakViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O comprimento da sequência deve ser pelo menos 1.")]
        public int LengthInDays { get; set; }
    }
}
