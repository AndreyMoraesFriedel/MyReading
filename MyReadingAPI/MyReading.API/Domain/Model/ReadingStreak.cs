using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReading.API.Domain.Model
{
    [Table("reading_streak")]
    public class ReadingStreak
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("user")]
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public int LengthInDays { get; set; }

        // Construtor com parâmetros
        public ReadingStreak(int id, int userId, DateTime startDate, DateTime? endDate, int lengthInDays)
        {
            Id = id;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            LengthInDays = lengthInDays;
        }

        // Construtor padrão necessário para a desserialização do JSON
        public ReadingStreak() { }
    }
}