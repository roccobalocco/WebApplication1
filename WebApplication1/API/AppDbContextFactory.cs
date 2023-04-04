using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using WebApplication1.Models;

namespace WebApplication1.API
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<MvcCoseinutiliContext>
    {
        public MvcCoseinutiliContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration
                .GetConnectionString("MvcCoseinutiliContext");

            optionsBuilder.UseSqlServer(connectionString);

            return new MvcCoseinutiliContext(optionsBuilder.Options);
        }
    }
}
