using Microsoft.EntityFrameworkCore;
using MyReading.API.Models;

namespace MyReading.API.Data
{
    public class MyReadingContext : DbContext
    {
        public MyReadingContext(DbContextOptions<MyReadingContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
