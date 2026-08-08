using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixOnModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Drivers_DriverId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ValidatorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Activities_ActivityId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Activities_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Drivers_DriverId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Users_ReviewerId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Vehicles_VehicleId",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "DrivingId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ValidatedBy",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "ReviewComments",
                table: "TripAnomalies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "TripAnomalies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidatedAt",
                table: "Activities",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Drivers_DriverId",
                table: "Activities",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ValidatorId",
                table: "Activities",
                column: "ValidatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Activities_ActivityId",
                table: "InvoiceLines",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Activities_ActivityId",
                table: "TripAnomalies",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Drivers_DriverId",
                table: "TripAnomalies",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Users_ReviewerId",
                table: "TripAnomalies",
                column: "ReviewerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Vehicles_VehicleId",
                table: "TripAnomalies",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Drivers_DriverId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_ValidatorId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Activities_ActivityId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Activities_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Drivers_DriverId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Users_ReviewerId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Vehicles_VehicleId",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "ReviewComments",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "TripAnomalies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidatedAt",
                table: "Activities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrivingId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValidatedBy",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityTypes_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId",
                principalTable: "ActivityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Drivers_DriverId",
                table: "Activities",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_ValidatorId",
                table: "Activities",
                column: "ValidatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Activities_ActivityId",
                table: "InvoiceLines",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Activities_ActivityId",
                table: "TripAnomalies",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Drivers_DriverId",
                table: "TripAnomalies",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Users_ReviewerId",
                table: "TripAnomalies",
                column: "ReviewerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Vehicles_VehicleId",
                table: "TripAnomalies",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
