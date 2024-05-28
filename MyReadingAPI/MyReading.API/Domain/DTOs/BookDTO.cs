using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Domain.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Capa { get; set; }
        public int Pages { get; set; }
    }
}
