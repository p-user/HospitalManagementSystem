using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctors.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctorSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Doctors",
                table: "Specializations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Doctors",
                table: "Specializations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "Doctors",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                schema: "Doctors",
                table: "Specializations",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Specializations_Name",
                schema: "Doctors",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "Doctors",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Doctors",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Doctors",
                table: "Specializations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
