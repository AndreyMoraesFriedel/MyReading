using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyReading.API.Data
{
    public class MyReadingContextFactory : IDesignTimeDbContextFactory<MyReadingContext>
    {
        public MyReadingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MyReadingContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));

            return new MyReadingContext(optionsBuilder.Options);
        }
    }
}
