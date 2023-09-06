using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hexagrams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hexagrams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    BaseHexagramId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangedHexagramId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Hexagrams_BaseHexagramId",
                        column: x => x.BaseHexagramId,
                        principalTable: "Hexagrams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Hexagrams_ChangedHexagramId",
                        column: x => x.ChangedHexagramId,
                        principalTable: "Hexagrams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    HexagramId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Texts_Hexagrams_HexagramId",
                        column: x => x.HexagramId,
                        principalTable: "Hexagrams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Texts_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineTexts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    TextId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineTexts_Texts_TextId",
                        column: x => x.TextId,
                        principalTable: "Texts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineTexts_TextId",
                table: "LineTexts",
                column: "TextId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BaseHexagramId",
                table: "Questions",
                column: "BaseHexagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ChangedHexagramId",
                table: "Questions",
                column: "ChangedHexagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Texts_HexagramId",
                table: "Texts",
                column: "HexagramId");

            migrationBuilder.CreateIndex(
                name: "IX_Texts_LanguageId",
                table: "Texts",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineTexts");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Texts");

            migrationBuilder.DropTable(
                name: "Hexagrams");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
