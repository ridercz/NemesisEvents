using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Altairis.NemesisEvents.DAL.Migrations
{
    public partial class EventTweaks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Venues",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "AdmissionFee",
                table: "Events",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "InfoUrl",
                table: "Events",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationUrl",
                table: "Events",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Events",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RegistrationUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Venues",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdmissionFee",
                table: "Events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
