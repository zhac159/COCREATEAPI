using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTrigramIndexOnNa432424 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_Users_AssigneeId",
                table: "ProjectRoles");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_Users_AssigneeId",
                table: "ProjectRoles",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_Users_AssigneeId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Projects");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>());

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_Users_AssigneeId",
                table: "ProjectRoles",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
