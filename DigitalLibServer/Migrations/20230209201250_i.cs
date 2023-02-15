using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalLibServer.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "B",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "nvarchar2(100)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar2(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_B", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "nvarchar2(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "nvarchar2(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookToGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GenreId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookToGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookToGenres_B_BookId",
                        column: x => x.BookId,
                        principalTable: "B",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookToGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookToTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BookId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TagId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookToTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookToTags_B_BookId",
                        column: x => x.BookId,
                        principalTable: "B",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookToTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookToGenres_BookId",
                table: "BookToGenres",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookToGenres_GenreId",
                table: "BookToGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookToTags_BookId",
                table: "BookToTags",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookToTags_TagId",
                table: "BookToTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookToGenres");

            migrationBuilder.DropTable(
                name: "BookToTags");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "B");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
