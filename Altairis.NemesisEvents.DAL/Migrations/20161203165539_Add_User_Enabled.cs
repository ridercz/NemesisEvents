using Microsoft.EntityFrameworkCore.Migrations;

namespace Altairis.NemesisEvents.DAL.Migrations {
    public partial class Add_User_Enabled : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AspNetUsers");
        }
    }
}
