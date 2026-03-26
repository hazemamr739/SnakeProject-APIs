using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)3);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UX_PsnCodesDenomination_Product_Region_Currency_Amount",
                table: "PsnCodesDenominations",
                columns: new[] { "ProductId", "RegionId", "Currency", "Amount" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Type",
                table: "Products",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SortOrder",
                table: "Categories",
                column: "SortOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "UX_PsnCodesDenomination_Product_Region_Currency_Amount",
                table: "PsnCodesDenominations");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_Type",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Products");
        }
    }
}
