using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalLibServer.Migrations
{
    /// <inheritdoc />
    public partial class tableRename2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Books");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookToGenres_Books_BookId",
                table: "BookToGenres",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookToTags_Books_BookId",
                table: "BookToTags",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookToGenres_Books_BookId",
                table: "BookToGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookToTags_Books_BookId",
                table: "BookToTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Books",
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
    }
}
