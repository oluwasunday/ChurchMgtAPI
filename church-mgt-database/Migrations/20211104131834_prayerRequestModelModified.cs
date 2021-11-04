using Microsoft.EntityFrameworkCore.Migrations;

namespace church_mgt_database.Migrations
{
    public partial class prayerRequestModelModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrayerRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PrayerRequests",
                type: "text",
                nullable: true);
        }
    }
}
