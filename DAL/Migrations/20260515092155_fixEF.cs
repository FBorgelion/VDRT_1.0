using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixEF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Activities_Activity_Id",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Activities_Activity_Id",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_TripAnomalies_Activity_Id",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLines_Activity_Id",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "Vehicle_Id",
                table: "VehicleAlerts");

            migrationBuilder.DropColumn(
                name: "Activity_Id",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "Driver_Id",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "Mission_Id",
                table: "TripAnomalies");

            migrationBuilder.DropColumn(
                name: "Employee_Id",
                table: "Timesheets");

            migrationBuilder.DropColumn(
                name: "Activity_Id",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "ActivityType_Id",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Driving_Id",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Vehicle_Id",
                table: "TripAnomalies",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "Site_Id",
                table: "Invoices",
                newName: "SiteId");

            migrationBuilder.RenameColumn(
                name: "Invoice_Id",
                table: "InvoiceLines",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "Mission_Id",
                table: "Activities",
                newName: "DrivingId");

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "TripAnomalies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_ActivityId",
                table: "InvoiceLines",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Activities_ActivityId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Activities_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_TripAnomalies_ActivityId",
                table: "TripAnomalies");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLines_ActivityId",
                table: "InvoiceLines");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "TripAnomalies",
                newName: "Vehicle_Id");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "Invoices",
                newName: "Site_Id");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "InvoiceLines",
                newName: "Invoice_Id");

            migrationBuilder.RenameColumn(
                name: "DrivingId",
                table: "Activities",
                newName: "Mission_Id");

            migrationBuilder.AddColumn<int>(
                name: "Vehicle_Id",
                table: "VehicleAlerts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "TripAnomalies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Activity_Id",
                table: "TripAnomalies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Driver_Id",
                table: "TripAnomalies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mission_Id",
                table: "TripAnomalies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Employee_Id",
                table: "Timesheets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Activity_Id",
                table: "InvoiceLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ActivityType_Id",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Driving_Id",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TripAnomalies_Activity_Id",
                table: "TripAnomalies",
                column: "Activity_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_Activity_Id",
                table: "InvoiceLines",
                column: "Activity_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Missions_MissionId",
                table: "Activities",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Activities_Activity_Id",
                table: "InvoiceLines",
                column: "Activity_Id",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Activities_Activity_Id",
                table: "TripAnomalies",
                column: "Activity_Id",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripAnomalies_Missions_MissionId",
                table: "TripAnomalies",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id");
        }
    }
}
