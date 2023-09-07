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
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hexagrams", x => x.Value);
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
                    BaseHexagramValue = table.Column<int>(type: "INTEGER", nullable: true),
                    ChangedHexagramValue = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Hexagrams_BaseHexagramValue",
                        column: x => x.BaseHexagramValue,
                        principalTable: "Hexagrams",
                        principalColumn: "Value");
                    table.ForeignKey(
                        name: "FK_Questions_Hexagrams_ChangedHexagramValue",
                        column: x => x.ChangedHexagramValue,
                        principalTable: "Hexagrams",
                        principalColumn: "Value");
                });

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Summary = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    HexagramValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Texts_Hexagrams_HexagramValue",
                        column: x => x.HexagramValue,
                        principalTable: "Hexagrams",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
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
                    MainTextId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineTexts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineTexts_Texts_MainTextId",
                        column: x => x.MainTextId,
                        principalTable: "Texts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineTexts_MainTextId",
                table: "LineTexts",
                column: "MainTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_BaseHexagramValue",
                table: "Questions",
                column: "BaseHexagramValue");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ChangedHexagramValue",
                table: "Questions",
                column: "ChangedHexagramValue");

            migrationBuilder.CreateIndex(
                name: "IX_Texts_HexagramValue",
                table: "Texts",
                column: "HexagramValue");

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
