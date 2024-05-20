using Microsoft.EntityFrameworkCore;
using MyReading.API.Models;

namespace MyReading.API.Data
{
    // Define o contexto do banco de dados para a aplicação
    public class MyReadingContext : DbContext
    {
        // Construtor que aceita opções de contexto
        public MyReadingContext(DbContextOptions<MyReadingContext> options) : base(options)
        {
        }

        // Define um conjunto de dados de livros, mapeado para a tabela "Books"
        public DbSet<Book> Books { get; set; }
    }
}
