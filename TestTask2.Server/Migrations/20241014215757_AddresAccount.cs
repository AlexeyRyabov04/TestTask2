using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddresAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "ReportFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReportFiles_AccountId",
                table: "ReportFiles",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFiles_Accounts_AccountId",
                table: "ReportFiles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportFiles_Accounts_AccountId",
                table: "ReportFiles");

            migrationBuilder.DropIndex(
                name: "IX_ReportFiles_AccountId",
                table: "ReportFiles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "ReportFiles");
        }
    }
}
