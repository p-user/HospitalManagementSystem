using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Departments.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doctors",
                schema: "Departments",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<Guid>(
                name: "HeadOfDepartment",
                schema: "Departments",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctors",
                schema: "Departments",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HeadOfDepartment",
                schema: "Departments",
                table: "Departments");
        }
    }
}
