using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPsnCodeInventoryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PsnCode_ProductId_IsUsed",
                table: "PsnCodes");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PsnCodes",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql("UPDATE [PsnCodes] SET [Status] = 3 WHERE [IsUsed] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PsnCode_ProductId_Status",
                table: "PsnCodes",
                columns: new[] { "ProductId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PsnCode_ProductId_Status",
                table: "PsnCodes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PsnCodes");

            migrationBuilder.CreateIndex(
                name: "IX_PsnCode_ProductId_IsUsed",
                table: "PsnCodes",
                columns: new[] { "ProductId", "IsUsed" });
        }
    }
}
