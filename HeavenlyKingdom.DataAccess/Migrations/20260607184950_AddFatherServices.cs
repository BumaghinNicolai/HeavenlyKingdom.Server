using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavenlyKingdom.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFatherServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Services",
                table: "Fathers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Services",
                table: "Fathers");
        }
    }
}
