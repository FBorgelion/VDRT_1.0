using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "Vehicle_Id",
                table: "Positions");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions");

            migrationBuilder.AddColumn<int>(
                name: "Vehicle_Id",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Vehicles_VehicleId",
                table: "Positions",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
