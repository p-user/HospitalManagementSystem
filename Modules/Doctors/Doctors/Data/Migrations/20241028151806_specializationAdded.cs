using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctors.Data.Migrations
{
    /// <inheritdoc />
    public partial class specializationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                schema: "Doctors",
                table: "Doctors");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializationId",
                schema: "Doctors",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Specializations",
                schema: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                schema: "Doctors",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                schema: "Doctors",
                table: "Doctors",
                column: "SpecializationId",
                principalSchema: "Doctors",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                schema: "Doctors",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specializations",
                schema: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecializationId",
                schema: "Doctors",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                schema: "Doctors",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                schema: "Doctors",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
