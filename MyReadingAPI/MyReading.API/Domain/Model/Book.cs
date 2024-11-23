using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReading.API.Domain.Model
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Capa { get; set; }

        public int Pages { get; set; }

        public Book(int id, string title, string author, string capa, int pages)
        {
            Id = id;
            Title = title;
            Author = author;
            Capa = capa;
            Pages = pages;

            ReadingProgresses = new List<ReadingProgress>();
        }

        // Construtor padrão necessário para a desserialização do JSON
        public Book() {
            ReadingProgresses = new List<ReadingProgress>();
        }

        public ICollection<ReadingProgress> ReadingProgresses { get; set; }
    }
}
