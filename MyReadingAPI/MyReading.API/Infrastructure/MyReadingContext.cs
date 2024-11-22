using Microsoft.EntityFrameworkCore;
using MyReading.API.Domain.Model;

namespace MyReading.API.Infrastructure
{
    public class MyReadingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }
        public DbSet<ReadingStreak> ReadingStreaks { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relacionamento entre ReadingProgress e User
            modelBuilder.Entity<ReadingProgress>()
                .HasOne<User>()
                .WithMany(u => u.ReadingProgresses)
                .HasForeignKey(rp => rp.UserId)
                .HasConstraintName("FK_ReadingProgress_User");

            // Configurar relacionamento entre ReadingProgress e Book
            modelBuilder.Entity<ReadingProgress>()
                .HasOne(rp => rp.Book) // Propriedade de navegação
                .WithMany(b => b.ReadingProgresses)
                .HasForeignKey(rp => rp.BookId)
                .HasConstraintName("FK_ReadingProgress_Book");

            // Configurar relacionamento entre ReadingStreak e User
            modelBuilder.Entity<ReadingStreak>()
                .HasOne<User>()
                .WithMany(u => u.ReadingStreaks) // Agora a propriedade existe
                .HasForeignKey(rs => rs.UserId)
                .HasConstraintName("FK_ReadingStreak_User");

            base.OnModelCreating(modelBuilder);
        }
    }
}
