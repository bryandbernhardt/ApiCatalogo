using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Bebidas', 'bebidas.jpg');");
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Lanches', 'lanches.jpg');");
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Sobremesas', 'sobremesas.jpg');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categories");
        }
    }
}
