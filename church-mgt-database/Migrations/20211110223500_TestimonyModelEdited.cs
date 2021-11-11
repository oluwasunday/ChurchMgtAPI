using Microsoft.EntityFrameworkCore.Migrations;

namespace church_mgt_database.Migrations
{
    public partial class TestimonyModelEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Testimonies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Testimonies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Testimonies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Testimonies");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Testimonies");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Testimonies");
        }
    }
}
