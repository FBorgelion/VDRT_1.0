using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class missionfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Drivers_DriverId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Sites_SiteId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Vehicles_VehicleId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Driver_Id",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Site_Id",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Vehicle_Id",
                table: "Missions");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Drivers_DriverId",
                table: "Missions",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Sites_SiteId",
                table: "Missions",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Vehicles_VehicleId",
                table: "Missions",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Drivers_DriverId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Sites_SiteId",
                table: "Missions");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Vehicles_VehicleId",
                table: "Missions");

            migrationBuilder.AddColumn<int>(
                name: "Driver_Id",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Site_Id",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vehicle_Id",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Drivers_DriverId",
                table: "Missions",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Sites_SiteId",
                table: "Missions",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Vehicles_VehicleId",
                table: "Missions",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
