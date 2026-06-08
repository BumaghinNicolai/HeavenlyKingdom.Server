using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavenlyKingdom.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AuditFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_Fathers_FatherId",
                table: "ServiceOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_Users_UserId",
                table: "ServiceOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Holidays",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_Fathers_FatherId",
                table: "ServiceOrders",
                column: "FatherId",
                principalTable: "Fathers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_Users_UserId",
                table: "ServiceOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_Fathers_FatherId",
                table: "ServiceOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOrders_Users_UserId",
                table: "ServiceOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Holidays",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_Fathers_FatherId",
                table: "ServiceOrders",
                column: "FatherId",
                principalTable: "Fathers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOrders_Users_UserId",
                table: "ServiceOrders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
