using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixOnModelCreatingV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Drivers_EmployeeId",
                table: "Timesheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Timesheets",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheets_EmployeeId",
                table: "Timesheets",
                newName: "IX_Timesheets_DriverId");

            migrationBuilder.AlterColumn<int>(
                name: "ApproverId",
                table: "Timesheets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Timesheets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Drivers_DriverId",
                table: "Timesheets",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Drivers_DriverId",
                table: "Timesheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets");

            migrationBuilder.DropIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Timesheets");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Timesheets",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheets_DriverId",
                table: "Timesheets",
                newName: "IX_Timesheets_EmployeeId");

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
                name: "FK_Timesheets_Drivers_EmployeeId",
                table: "Timesheets",
                column: "EmployeeId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_ApproverId",
                table: "Timesheets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
