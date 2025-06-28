using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Add_user_reference_to_the_AccountBalances_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Balances",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Balances_UserId",
                table: "Balances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Balances_Users_UserId",
                table: "Balances");

            migrationBuilder.DropIndex(
                name: "IX_Balances_UserId",
                table: "Balances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Balances");
        }
    }
}
