using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPhoto.Migrations
{
    public partial class ChangedPlacePhotoEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhotoEntities_AlbumEntityId",
                table: "PhotoEntities");

            migrationBuilder.DropIndex(
                name: "IX_PhotoEntities_PlaceEntityId",
                table: "PhotoEntities");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_AlbumEntityId",
                table: "PhotoEntities",
                column: "AlbumEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_PlaceEntityId",
                table: "PhotoEntities",
                column: "PlaceEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PhotoEntities_AlbumEntityId",
                table: "PhotoEntities");

            migrationBuilder.DropIndex(
                name: "IX_PhotoEntities_PlaceEntityId",
                table: "PhotoEntities");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_AlbumEntityId",
                table: "PhotoEntities",
                column: "AlbumEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoEntities_PlaceEntityId",
                table: "PhotoEntities",
                column: "PlaceEntityId",
                unique: true);
        }
    }
}
