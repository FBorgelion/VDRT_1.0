using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Drivers_DriverId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DriverId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DriverId",
                table: "Invoices",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Drivers_DriverId",
                table: "Invoices",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
