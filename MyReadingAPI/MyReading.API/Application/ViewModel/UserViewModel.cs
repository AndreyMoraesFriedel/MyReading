using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Application.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public IFormFile? Photo { get; set; }

        public TimeSpan TotalReadingTime { get; set; }
    }
}
