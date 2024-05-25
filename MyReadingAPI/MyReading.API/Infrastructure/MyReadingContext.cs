using Microsoft.EntityFrameworkCore;
using MyReading.API.Model;


namespace MyReading.API.Infrastructure
{
    public class MyReadingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var str = $"Server=localhost;" +
             $"Database=myreadingdb;" +
             $"Uid=root;" +
             $"Pwd=";
            optionsBuilder.UseMySql(str
                , ServerVersion.AutoDetect(str)
            );
        }
    }
}
