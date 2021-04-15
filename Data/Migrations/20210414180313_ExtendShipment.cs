using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSmart.Data.Migrations
{
    public partial class ExtendShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Shipments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Shipments");
        }
    }
}
