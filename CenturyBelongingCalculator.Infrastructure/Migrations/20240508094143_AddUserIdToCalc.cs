using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenturyBelongingCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToCalc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Calcs",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Calcs");
        }
    }
}
