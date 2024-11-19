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

        [Required]
        public int PagesRead { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // Construtor com parâmetros
        public ReadingProgress(int id, int userId, int bookId, int pagesRead, DateTime? startDate, DateTime? endDate)
        {
            Id = id;
            UserId = userId;
            BookId = bookId;
            PagesRead = pagesRead;
            StartDate = startDate;
            EndDate = endDate;
        }

        // Construtor padrão necessário para a desserialização do JSON
        public ReadingProgress() { }
    }
}