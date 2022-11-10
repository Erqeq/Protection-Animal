using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Protection_Animal.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Protection.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationCategory> Categories { get; set; }
        public DbSet<Client> Clients { get;set; }
        public AppDbContext(DbContextOptions<AppDbContext> ctx) : base(ctx)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config.GetConnectionString("ConnectionString");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
