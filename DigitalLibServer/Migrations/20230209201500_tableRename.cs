using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalLibServer.Migrations
{
    /// <inheritdoc />
    public partial class tableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookToGenres_B_BookId",
                table: "BookToGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookToTags_B_BookId",
                table: "BookToTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_B",
                table: "B");

            migrationBuilder.RenameTable(
                name: "B",
                newName: "BooksTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BooksTable",
                table: "BooksTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookToGenres_BooksTable_BookId",
                table: "BookToGenres",
                column: "BookId",
                principalTable: "BooksTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookToTags_BooksTable_BookId",
                table: "BookToTags",
                column: "BookId",
                principalTable: "BooksTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookToGenres_BooksTable_BookId",
                table: "BookToGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookToTags_BooksTable_BookId",
                table: "BookToTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BooksTable",
                table: "BooksTable");

            migrationBuilder.RenameTable(
                name: "BooksTable",
                newName: "B");

            migrationBuilder.AddPrimaryKey(
                name: "PK_B",
                table: "B",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookToGenres_B_BookId",
                table: "BookToGenres",
                column: "BookId",
                principalTable: "B",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookToTags_B_BookId",
                table: "BookToTags",
                column: "BookId",
                principalTable: "B",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
