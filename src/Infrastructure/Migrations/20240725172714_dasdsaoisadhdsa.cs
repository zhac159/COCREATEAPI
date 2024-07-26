using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dasdsaoisadhdsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Coins",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 10000);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Coins",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 10000,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());
        }
    }
}
