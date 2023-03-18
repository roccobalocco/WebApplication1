using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApplication1.Models
{
    public class MvcCoseinutiliContext : DbContext
    {
        public MvcCoseinutiliContext(DbContextOptions<MvcCoseinutiliContext> options) : base(options) { }

        public DbSet<Utenti> Utentis { get; set; }
        public DbSet<Commenti> Commentis { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Commenti_Categorie> CommentiCategories { get; set; }
        public DbSet<Tag_Utenti> TagUtentis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("MvcCoseinutiliContext");
                optionsBuilder.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);
            }
        }
        
        
    }
}