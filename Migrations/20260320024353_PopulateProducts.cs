using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId)" +
                                 "VALUES ('Coca-Cola Diet', 'Refrigerante de Cola 350ml', 5.45, 'cocacola.jpg', 50, now(), 1)");
            
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId)" +
                                 "VALUES ('Lanche de Atum', 'Lanche de Atum com maionese 350g', 8.50, 'atum.jpg', 10, now(), 2)");
            
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, CreatedAt, CategoryId)" +
                                 "VALUES ('Pudim 100g', 'Pudim de leite condensado 100g', 6.75, 'pudim.jpg', 20, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
