using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReading.API.Model
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }  // setter público

        [Required]
        public string Author { get; set; }  // setter público

        public int Pages { get; set; }  // setter público

        public DateTime? DateRead { get; set; }  // setter público

        public Book(int id, string title, string author, int pages, DateTime? dateRead)
        {
            Id = id;
            Title = title;
            Author = author;
            Pages = pages;
            DateRead = dateRead;
        }

        // Construtor padrão necessário para a desserialização do JSON
        public Book() { }
    }
}
