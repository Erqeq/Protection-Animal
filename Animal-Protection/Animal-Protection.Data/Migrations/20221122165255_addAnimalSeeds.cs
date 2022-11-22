using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animal_Protection.Data.Migrations
{
    public partial class addAnimalSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Млекопитающее семейства кошачьих отряда хищных", "213f5974-f6f2-4df7-bd92-5aac6af79919.jpg", "Кошка" },
                    { 2, "Плацентарное млекопитающее отряда хищных семейства псовых", "2268e547-bf9b-41a7-9bbf-cc49f056171a.jpg", "Собака" },
                    { 3, "Животное из семейства лошадиных отряда непарнокопытных", "9b28c272-d3b3-432a-9a41-59c793511c81.jpg", "Лошадь" },
                    { 4, "Отряд птиц из инфракласса новонёбных", "793e7730-31ab-4384-a88b-9c1f100337d4.jpg", "Попугай" },
                    { 5, "Вид млекопитающих из рода евразийских ежей семейства ежовых", "29659e5f-cd62-41b3-9c67-9fea4e553f16.jpg", "Ёжик" },
                    { 6, "Семейство млекопитающих из отряда грызунов", "aadadb39-bdad-4b69-b917-01d11e7bf72e.jpg", "Мышь" },
                    { 7, "Общее название нескольких родов млекопитающих из семейства зайцевых", "b16b581e-465b-44ba-a565-17d8d5464ae8.png", "Кролик" },
                    { 8, "Подсемейство грызунов семейства хомяковых", "cc09001e-3027-49d9-b350-c6e0f7d189a1.jpg", "Хомяк" },
                    { 9, "Парафилетическая группа пресмыкающихся отряда чешуйчатых", "e4722329-15d2-4038-8689-bdf3d760a9a2.png", "Ящерица" },
                    { 10, "Парафилетическая группа водных позвоночных животных", "7d1975d5-cd16-4949-9670-7025f16650d0.jpg", "Рыбка" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
