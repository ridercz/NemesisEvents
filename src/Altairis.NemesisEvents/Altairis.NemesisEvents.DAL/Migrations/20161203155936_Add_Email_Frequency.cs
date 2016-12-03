using Microsoft.EntityFrameworkCore.Migrations;

namespace Altairis.NemesisEvents.DAL.Migrations {
    public partial class Add_Email_Frequency : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<int>(
                name: "EmailFrequency",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql("UPDATE AspNetUsers SET EmailFrequency = 3 WHERE SendWeeklyMessages = 1");
            migrationBuilder.Sql("UPDATE AspNetUsers SET EmailFrequency = 2 WHERE SendDailyMessages = 1");
            migrationBuilder.Sql("UPDATE AspNetUsers SET EmailFrequency = 1 WHERE SendSingleMessages = 1");

            migrationBuilder.DropColumn(
                name: "SendDailyMessages",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SendSingleMessages",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SendWeeklyMessages",
                table: "AspNetUsers");

        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<bool>(
                name: "SendDailyMessages",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendSingleMessages",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendWeeklyMessages",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE AspNetUsers SET SendWeeklyMessages = 1 WHERE EmailFrequency = 3");
            migrationBuilder.Sql("UPDATE AspNetUsers SET SendDailyMessages = 1 WHERE EmailFrequency = 2");
            migrationBuilder.Sql("UPDATE AspNetUsers SET SendSingleMessages = 1 WHERE EmailFrequency = 1");

            migrationBuilder.DropColumn(
                name: "EmailFrequency",
                table: "AspNetUsers");
        }
    }
}
