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

            builder.Entity<Animal>().HasData(new Animal
            {
                Id = 1,
                Name = "Кошка",
                Description = "Млекопитающее семейства кошачьих отряда хищных",
                Image = "213f5974-f6f2-4df7-bd92-5aac6af79919.jpg"
            },
            new Animal
            {
                Id = 2,
                Name = "Собака",
                Description = "Плацентарное млекопитающее отряда хищных семейства псовых",
                Image = "2268e547-bf9b-41a7-9bbf-cc49f056171a.jpg"
            },
            new Animal
            {
                Id = 3,
                Name = "Лошадь",
                Description = "Животное из семейства лошадиных отряда непарнокопытных",
                Image = "9b28c272-d3b3-432a-9a41-59c793511c81.jpg"
            },
            new Animal
            {
                Id = 4,
                Name = "Попугай",
                Description = "Отряд птиц из инфракласса новонёбных",
                Image = "793e7730-31ab-4384-a88b-9c1f100337d4.jpg"
            },
            new Animal
            {
                Id = 5,
                Name = "Ёжик",
                Description = "Вид млекопитающих из рода евразийских ежей семейства ежовых",
                Image = "29659e5f-cd62-41b3-9c67-9fea4e553f16.jpg"
            },
            new Animal
            {
                Id = 6,
                Name = "Мышь",
                Description = "Семейство млекопитающих из отряда грызунов",
                Image = "aadadb39-bdad-4b69-b917-01d11e7bf72e.jpg"
            },
            new Animal
            {
                Id = 7,
                Name = "Кролик",
                Description = "Общее название нескольких родов млекопитающих из семейства зайцевых",
                Image = "b16b581e-465b-44ba-a565-17d8d5464ae8.png"
            },
            new Animal
            {
                Id = 8,
                Name = "Хомяк",
                Description = "Подсемейство грызунов семейства хомяковых",
                Image = "cc09001e-3027-49d9-b350-c6e0f7d189a1.jpg"
            },
            new Animal
            {
                Id = 9,
                Name = "Ящерица",
                Description = "Парафилетическая группа пресмыкающихся отряда чешуйчатых",
                Image = "e4722329-15d2-4038-8689-bdf3d760a9a2.png"
            },
            new Animal
            {
                Id = 10,
                Name = "Рыбка",
                Description = "Парафилетическая группа водных позвоночных животных",
                Image = "7d1975d5-cd16-4949-9670-7025f16650d0.jpg"
            });
        }
    }
}

