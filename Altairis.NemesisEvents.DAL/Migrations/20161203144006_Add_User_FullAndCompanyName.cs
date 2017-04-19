using Microsoft.EntityFrameworkCore.Migrations;

namespace Altairis.NemesisEvents.DAL.Migrations {
    public partial class Add_User_FullAndCompanyName : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
