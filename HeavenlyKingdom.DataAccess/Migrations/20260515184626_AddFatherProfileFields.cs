using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavenlyKingdom.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFatherProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diocese",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Parish",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Fathers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fathers_UserId",
                table: "Fathers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Fathers_Users_UserId",
                table: "Fathers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fathers_Users_UserId",
                table: "Fathers");

            migrationBuilder.DropIndex(
                name: "IX_Fathers_UserId",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "Diocese",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "Parish",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Fathers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Fathers");
        }
    }
}
