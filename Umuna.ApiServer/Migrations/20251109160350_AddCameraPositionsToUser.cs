using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Umuna.ApiServer.Migrations
{
    /// <inheritdoc />
    public partial class AddCameraPositionsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CameraPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position_X = table.Column<float>(type: "REAL", nullable: false),
                    Position_Y = table.Column<float>(type: "REAL", nullable: false),
                    Position_Z = table.Column<float>(type: "REAL", nullable: false),
                    Rotation_X = table.Column<float>(type: "REAL", nullable: false),
                    Rotation_Y = table.Column<float>(type: "REAL", nullable: false),
                    Rotation_Z = table.Column<float>(type: "REAL", nullable: false),
                    Rotation_W = table.Column<float>(type: "REAL", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CameraPosition_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CameraPosition_UserId",
                table: "CameraPosition",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CameraPosition");
        }
    }
}
