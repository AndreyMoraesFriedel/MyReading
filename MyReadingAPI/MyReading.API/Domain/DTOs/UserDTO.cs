using System.ComponentModel.DataAnnotations;

namespace MyReading.API.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
}
