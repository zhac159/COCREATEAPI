using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SeenMatches_UserId",
                table: "SeenMatches");

            migrationBuilder.CreateIndex(
                name: "IX_SeenMatches_UserId_ProjectRoleId",
                table: "SeenMatches",
                columns: new[] { "UserId", "ProjectRoleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SeenMatches_UserId_ProjectRoleId",
                table: "SeenMatches");

            migrationBuilder.CreateIndex(
                name: "IX_SeenMatches_UserId",
                table: "SeenMatches",
                column: "UserId");
        }
    }
}
