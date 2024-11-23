using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReading.API.Domain.Model
{
    [Table("reading_progress")]
    public class ReadingProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("user")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("book")]
        public int BookId { get; set; }

        /*
        [Required]
        public int PagesRead { get; set; }
        */
        [Required]
        public int TotalReadingTime { get; set; }

        // Propriedade de navegação para Book
        public Book Book { get; set; }

        public ReadingProgress(int id, int userId, int bookId, int totalReadingTime)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            /*PagesRead = pagesRead;*/
            TotalReadingTime = totalReadingTime;
        }

        public ReadingProgress() { }
    }
}