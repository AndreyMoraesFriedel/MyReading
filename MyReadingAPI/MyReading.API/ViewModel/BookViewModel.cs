using System.ComponentModel.DataAnnotations;

namespace MyReading.API.ViewModel
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }  // setter público

        [Required]
        public string? Author { get; set; }  // setter público

        public int Pages { get; set; }  // setter público

        public DateTime? DateRead { get; set; }  // setter público
    }
}
