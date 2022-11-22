using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Protection_Animal.Model.Entities;

namespace Animal_Protection.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationCategory> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> ctx) : base(ctx)
        {

        }
        public AppDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("ConnectionString");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationCategory>().HasData(
                new ApplicationCategory { Id = 1, Name = "Продажа" },
                new ApplicationCategory { Id = 2, Name = "Передержка" },
                new ApplicationCategory { Id = 3, Name = "Такси для животных" },
                new ApplicationCategory { Id = 4, Name = "Случка" },
                new ApplicationCategory { Id = 5, Name = "Подарок" },
                new ApplicationCategory { Id = 6, Name = "Другое" });


        }
    }
}
