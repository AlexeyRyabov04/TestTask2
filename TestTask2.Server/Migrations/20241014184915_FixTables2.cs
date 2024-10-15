using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask2.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ReportFiles_ReportFileId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ReportFileId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ReportFileId",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsClassResult",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroupResult",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClassResult",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsGroupResult",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Classes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportFileId",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ReportFileId",
                table: "Accounts",
                column: "ReportFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_ReportFiles_ReportFileId",
                table: "Accounts",
                column: "ReportFileId",
                principalTable: "ReportFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
