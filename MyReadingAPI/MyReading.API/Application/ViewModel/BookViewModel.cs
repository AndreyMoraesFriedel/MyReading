using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Application.ViewModel
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public IFormFile? Capa { get; set; }

        public int Pages { get; set; }
    }
}
