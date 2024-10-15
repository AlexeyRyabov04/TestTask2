using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask2.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRecordResult",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecordResult",
                table: "Accounts");
        }
    }
}
