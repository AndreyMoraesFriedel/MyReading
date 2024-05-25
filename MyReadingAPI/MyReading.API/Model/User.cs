using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReading.API.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Photo { get; set; }

        public User(int id, string name, string email, string password, string photo)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Photo = photo;
        }

        public User() { }
    }
}
