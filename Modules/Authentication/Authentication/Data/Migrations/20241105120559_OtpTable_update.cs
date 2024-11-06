using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Data.Migrations
{
    /// <inheritdoc />
    public partial class OtpTable_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Authentication",
                table: "Otps",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_UserId",
                schema: "Authentication",
                table: "Otps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                schema: "Authentication",
                table: "Otps",
                column: "UserId",
                principalSchema: "Authentication",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otps_AspNetUsers_UserId",
                schema: "Authentication",
                table: "Otps");

            migrationBuilder.DropIndex(
                name: "IX_Otps_UserId",
                schema: "Authentication",
                table: "Otps");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Authentication",
                table: "Otps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
