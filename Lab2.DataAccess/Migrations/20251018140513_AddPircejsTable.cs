using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPircejsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PircejsId",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pircejs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pircejs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pircejs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_PircejsId",
                table: "Baskets",
                column: "PircejsId");

            migrationBuilder.CreateIndex(
                name: "IX_Pircejs_ProductId",
                table: "Pircejs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Pircejs_PircejsId",
                table: "Baskets",
                column: "PircejsId",
                principalTable: "Pircejs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Pircejs_PircejsId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "Pircejs");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_PircejsId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PircejsId",
                table: "Baskets");
        }
    }
}
