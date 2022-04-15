using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPhoto.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhotoCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Tags = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "#none"),
                    CreationDate = table.Column<string>(type: "TEXT", nullable: false),
                    IsLocal = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    ResolutionWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    ResolutionHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    Size = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AlbumEntityId = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    Tags = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "#none"),
                    DateTaken = table.Column<string>(type: "TEXT", nullable: false),
                    PlaceEntityId = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
                    ImageEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoEntities_AlbumEntities_AlbumEntityId",
                        column: x => x.AlbumEntityId,
                        principalTable: "AlbumEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PhotoEntities_ImageEntities_ImageEntityId",
                        column: x => x.ImageEntityId,
                        principalTable: "ImageEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoEntities_PlaceEntities_PlaceEntityId",
                        column: x => x.PlaceEntityId,
                        principalTable: "PlaceEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_AlbumEntityId",
                table: "PhotoEntities",
                column: "AlbumEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_ImageEntityId",
                table: "PhotoEntities",
                column: "ImageEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_PlaceEntityId",
                table: "PhotoEntities",
                column: "PlaceEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoEntities");

            migrationBuilder.DropTable(
                name: "AlbumEntities");

            migrationBuilder.DropTable(
                name: "ImageEntities");

            migrationBuilder.DropTable(
                name: "PlaceEntities");
        }
    }
}
