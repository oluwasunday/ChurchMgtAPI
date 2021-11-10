using Microsoft.EntityFrameworkCore.Migrations;

namespace church_mgt_database.Migrations
{
    public partial class PaymentModelEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Payments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Payments");
        }
    }
}
