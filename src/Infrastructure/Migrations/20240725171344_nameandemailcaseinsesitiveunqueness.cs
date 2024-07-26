using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nameandemailcaseinsesitiveunqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Users_Email", table: "Users");

            migrationBuilder.DropIndex(name: "IX_Users_Username", table: "Users");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>()
            );

            migrationBuilder.Sql(
                "CREATE UNIQUE INDEX \"IX_Users_Username_Lower\" ON \"Users\" (LOWER(\"Username\"));"
            );
            migrationBuilder.Sql(
                "CREATE UNIQUE INDEX \"IX_Users_Email_Lower\" ON \"Users\" (LOWER(\"Email\"));"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Keywords",
                table: "Skills",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>(),
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldDefaultValue: new List<string>()
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true
            );

            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_Users_Username_Lower\";");
            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_Users_Email_Lower\";");
        }
    }
}
