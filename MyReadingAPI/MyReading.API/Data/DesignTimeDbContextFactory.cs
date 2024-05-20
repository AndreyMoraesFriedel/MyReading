using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace MyReading.API.Data
{
    public class MyReadingContextFactory : IDesignTimeDbContextFactory<MyReadingContext>
    {
        public MyReadingContext CreateDbContext(string[] args)
        {
            // Cria uma configuração para obter a string de conexão
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obtém a string de conexão do arquivo appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configura as opções do DbContext
            var optionsBuilder = new DbContextOptionsBuilder<MyReadingContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

            return new MyReadingContext(optionsBuilder.Options);
        }
    }
}
