using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnakeProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1
                    FROM sys.foreign_keys
                    WHERE name = 'FK_PsnCodesDenominations_Categories_CategoryId'
                )
                BEGIN
                    ALTER TABLE [PsnCodesDenominations]
                    DROP CONSTRAINT [FK_PsnCodesDenominations_Categories_CategoryId];
                END
                """);

            migrationBuilder.Sql("""
                IF EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = 'IX_PsnCodesDenominations_CategoryId'
                    AND object_id = OBJECT_ID('PsnCodesDenominations')
                )
                BEGIN
                    DROP INDEX [IX_PsnCodesDenominations_CategoryId] ON [PsnCodesDenominations];
                END
                """);

            migrationBuilder.Sql("""
                IF COL_LENGTH('PsnCodesDenominations', 'CategoryId') IS NOT NULL
                BEGIN
                    ALTER TABLE [PsnCodesDenominations]
                    DROP COLUMN [CategoryId];
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH('PsnCodesDenominations', 'CategoryId') IS NULL
                BEGIN
                    ALTER TABLE [PsnCodesDenominations]
                    ADD [CategoryId] int NULL;
                END
                """);

            migrationBuilder.Sql("""
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.indexes
                    WHERE name = 'IX_PsnCodesDenominations_CategoryId'
                    AND object_id = OBJECT_ID('PsnCodesDenominations')
                )
                BEGIN
                    CREATE INDEX [IX_PsnCodesDenominations_CategoryId]
                    ON [PsnCodesDenominations]([CategoryId]);
                END
                """);

            migrationBuilder.Sql("""
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.foreign_keys
                    WHERE name = 'FK_PsnCodesDenominations_Categories_CategoryId'
                )
                BEGIN
                    ALTER TABLE [PsnCodesDenominations]
                    ADD CONSTRAINT [FK_PsnCodesDenominations_Categories_CategoryId]
                    FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]);
                END
                """);
        }
    }
}
