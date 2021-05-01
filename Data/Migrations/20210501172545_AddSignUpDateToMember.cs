using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookSmart.Data.Migrations
{
    public partial class AddSignUpDateToMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MembershipExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignUpDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipExpiration",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SignUpDate",
                table: "AspNetUsers");
        }
    }
}
