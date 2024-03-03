using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ProjectRoles");

            migrationBuilder.RenameColumn(
                name: "FileSrcs",
                table: "ProjectRoles",
                newName: "Keywords");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ProjectRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "ProjectRoles",
                type: "geometry",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProjectRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ProjectRoleMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uri = table.Column<string>(type: "text", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ProjectRoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRoleMedias_ProjectRoles_ProjectRoleId",
                        column: x => x.ProjectRoleId,
                        principalTable: "ProjectRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleMedias_MediaType",
                table: "ProjectRoleMedias",
                column: "MediaType");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleMedias_ProjectRoleId",
                table: "ProjectRoleMedias",
                column: "ProjectRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRoleMedias");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProjectRoles");

            migrationBuilder.RenameColumn(
                name: "Keywords",
                table: "ProjectRoles",
                newName: "FileSrcs");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ProjectRoles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ProjectRoles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
