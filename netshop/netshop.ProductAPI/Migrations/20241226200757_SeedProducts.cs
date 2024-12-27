using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netshop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                "Values('Caderno',7.55, 'Caderno', 10, 'caderno.jpg', 1);");

            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                "Values('carteira',20.00, 'carteira', 2, 'carteiro.jpg', 2);");

            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId) " +
                "Values('Borracha',8.80, 'borracha', 15, 'borracha.jpg', 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {

        }
    }
}
