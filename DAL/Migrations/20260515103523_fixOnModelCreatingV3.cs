using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixOnModelCreatingV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Timesheets");

            migrationBuilder.AlterColumn<int>(
                name: "ApproverId",
                table: "Timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets");

            migrationBuilder.AlterColumn<int>(
                name: "ApproverId",
                table: "Timesheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
