using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Users_UserId",
                table: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_Enquiries_UserId_ProjectRoleId",
                table: "Enquiries");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Enquiries",
                newName: "ProjectManagerId");

            migrationBuilder.AddColumn<int>(
                name: "EnquirerId",
                table: "Enquiries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_EnquirerId_ProjectRoleId",
                table: "Enquiries",
                columns: new[] { "EnquirerId", "ProjectRoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_ProjectManagerId",
                table: "Enquiries",
                column: "ProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Users_EnquirerId",
                table: "Enquiries",
                column: "EnquirerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Users_ProjectManagerId",
                table: "Enquiries",
                column: "ProjectManagerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Users_EnquirerId",
                table: "Enquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Users_ProjectManagerId",
                table: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_Enquiries_EnquirerId_ProjectRoleId",
                table: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_Enquiries_ProjectManagerId",
                table: "Enquiries");

            migrationBuilder.DropColumn(
                name: "EnquirerId",
                table: "Enquiries");

            migrationBuilder.RenameColumn(
                name: "ProjectManagerId",
                table: "Enquiries",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_UserId_ProjectRoleId",
                table: "Enquiries",
                columns: new[] { "UserId", "ProjectRoleId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Users_UserId",
                table: "Enquiries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
