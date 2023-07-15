using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetRPG.Migrations
{
    /// <inheritdoc />
    public partial class addHighScoreCalculatePropertiesToCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victorys",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "Victorys",
                table: "characters");
        }
    }
}
