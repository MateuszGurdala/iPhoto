using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iPhoto.Migrations
{
    public partial class PlaceEntitiesAddMapMarkerColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapMarkerColor",
                table: "PlaceEntities",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapMarkerColor",
                table: "PlaceEntities");
        }
    }
}
