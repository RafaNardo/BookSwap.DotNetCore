using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSwap.BooksService.Modules.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "Book",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreId",
                table: "Book",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookGenre_GenreId",
                table: "Book",
                column: "GenreId",
                principalTable: "BookGenre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookGenre_GenreId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropIndex(
                name: "IX_Book_GenreId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Book");
        }
    }
}
