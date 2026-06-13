using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildLinkApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class removePhoneAndAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
