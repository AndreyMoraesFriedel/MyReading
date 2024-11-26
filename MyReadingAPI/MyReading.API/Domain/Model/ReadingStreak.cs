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

        public DateTime LastStreak { get; set; }

        public DateTime? EndDate { get; set; }

        public int LengthInDays { get; set; }

        // Construtor com parâmetros
        public ReadingStreak(int id, int userId)
        {
            Id = id;
            UserId = userId;
            LastStreak = DateTime.MinValue; 
            LengthInDays = 0;
        }

        // Construtor padrão necessário para a desserialização do JSON
        public ReadingStreak()
        {
            LastStreak = DateTime.MinValue;
            LengthInDays = 0;
        }
    }
}