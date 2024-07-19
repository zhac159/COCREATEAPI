using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fffddf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherCodes",
                table: "VoucherCodes");

            migrationBuilder.DropIndex(
                name: "IX_VoucherCodes_Code",
                table: "VoucherCodes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VoucherCodes");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherCodes",
                table: "VoucherCodes",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VoucherCodes",
                table: "VoucherCodes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VoucherCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoucherCodes",
                table: "VoucherCodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherCodes_Code",
                table: "VoucherCodes",
                column: "Code",
                unique: true);
        }
    }
}
