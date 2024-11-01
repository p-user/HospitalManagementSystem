using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctors.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHeadOfDepartment",
                schema: "Doctors",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHeadOfDepartment",
                schema: "Doctors",
                table: "Doctors");
        }
    }
}
