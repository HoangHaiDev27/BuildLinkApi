using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildLinkApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAccountRoleToOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM order_items;");
            migrationBuilder.Sql("DELETE FROM orders;");
            migrationBuilder.Sql("DELETE FROM addresses;");
            migrationBuilder.Sql("DELETE FROM refresh_tokens;");
            migrationBuilder.Sql("DELETE FROM account_roles;");
            migrationBuilder.Sql("DELETE FROM accounts;");

            migrationBuilder.DropTable(
                name: "account_roles");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_accounts_RoleId",
                table: "accounts",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_roles_RoleId",
                table: "accounts",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_roles_RoleId",
                table: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_accounts_RoleId",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "accounts");

            migrationBuilder.CreateTable(
                name: "account_roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_roles_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_roles_AccountId_RoleId",
                table: "account_roles",
                columns: new[] { "AccountId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_roles_RoleId",
                table: "account_roles",
                column: "RoleId");
        }
    }
}
