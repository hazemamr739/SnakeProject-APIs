using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigureSubscriptionAndGameShareMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameShareAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessType = table.Column<byte>(type: "tinyint", nullable: false),
                    Console = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameShareAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameShareAccount_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PlusSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plan = table.Column<byte>(type: "tinyint", nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    AccessType = table.Column<byte>(type: "tinyint", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlusSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlusSubscription_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameShareAccount_CategoryId",
                table: "GameShareAccount",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameShareAccount_IsActive",
                table: "GameShareAccount",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_PlusSubscription_CategoryId",
                table: "PlusSubscription",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlusSubscription_IsActive",
                table: "PlusSubscription",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameShareAccount");

            migrationBuilder.DropTable(
                name: "PlusSubscription");
        }
    }
}
