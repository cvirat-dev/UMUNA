using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umuna.ApiServer.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerEmail",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PlayerPassword",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "PlayerName",
                table: "Users",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PlayerPassword");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "PlayerName");

            migrationBuilder.AddColumn<string>(
                name: "PlayerEmail",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
