using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Application.ViewModel
{
    public class ReadingProgressViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        /*
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O número de páginas lidas deve ser um valor positivo.")]
        public int PagesRead { get; set; }
        */

        [Required]
        public int TotalReadingTime { get; set; }
    }
}
