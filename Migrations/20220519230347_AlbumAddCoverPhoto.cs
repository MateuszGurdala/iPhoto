using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPhoto.Migrations
{
    public partial class AlbumAddCoverPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageEntityId",
                table: "AlbumEntities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlbumEntities_ImageEntityId",
                table: "AlbumEntities",
                column: "ImageEntityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "AlbumImageFK",
                table: "AlbumEntities",
                column: "ImageEntityId",
                principalTable: "ImageEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "AlbumImageFK",
                table: "AlbumEntities");

            migrationBuilder.DropIndex(
                name: "IX_AlbumEntities_ImageEntityId",
                table: "AlbumEntities");

            migrationBuilder.DropColumn(
                name: "ImageEntityId",
                table: "AlbumEntities");
        }
    }
}
