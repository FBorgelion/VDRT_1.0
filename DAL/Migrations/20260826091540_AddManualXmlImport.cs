using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddManualXmlImport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalFileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    OriginalFileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileHash = table.Column<string>(type: "char(64)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalFiles = table.Column<int>(type: "int", nullable: false),
                    SuccessfulFiles = table.Column<int>(type: "int", nullable: false),
                    FailedFiles = table.Column<int>(type: "int", nullable: false),
                    ImportedTraceCount = table.Column<int>(type: "int", nullable: false),
                    RejectedTraceCount = table.Column<int>(type: "int", nullable: false),
                    SkippedTraceCount = table.Column<int>(type: "int", nullable: false),
                    TechnicalMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportBatches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportSourceFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportBatchId = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ContentHash = table.Column<string>(type: "char(64)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ImportedTraceCount = table.Column<int>(type: "int", nullable: false),
                    RejectedTraceCount = table.Column<int>(type: "int", nullable: false),
                    SkippedTraceCount = table.Column<int>(type: "int", nullable: false),
                    TechnicalMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSourceFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportSourceFiles_ImportBatches_ImportBatchId",
                        column: x => x.ImportBatchId,
                        principalTable: "ImportBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportedTraces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportSourceFileId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TraceTypeRaw = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TraceType = table.Column<int>(type: "int", nullable: true),
                    SourceRaw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TechnicalTimeRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TechnicalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LatitudeRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true),
                    LongitudeRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true),
                    MileageRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mileage = table.Column<long>(type: "bigint", nullable: true),
                    HeadingRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Heading = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    SpeedRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Speed = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: true),
                    LinkId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActivityCode = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DriverIdsRaw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SequenceRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sequence = table.Column<long>(type: "bigint", nullable: true),
                    ActivityStartTimeRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActivityStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActivityLengthMillisecondsRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActivityLengthMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    DrivingLengthMillisecondsRaw = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DrivingLengthMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    DeviceRaw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActivityReportRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityFinalReportRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceHash = table.Column<string>(type: "char(64)", nullable: false),
                    RawXml = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedTraces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportedTraces_ImportSourceFiles_ImportSourceFileId",
                        column: x => x.ImportSourceFileId,
                        principalTable: "ImportSourceFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportErrors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportBatchId = table.Column<int>(type: "int", nullable: false),
                    ImportSourceFileId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TracePosition = table.Column<int>(type: "int", nullable: true),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportErrors_ImportBatches_ImportBatchId",
                        column: x => x.ImportBatchId,
                        principalTable: "ImportBatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportErrors_ImportSourceFiles_ImportSourceFileId",
                        column: x => x.ImportSourceFileId,
                        principalTable: "ImportSourceFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportedTraceProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportedTraceId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    KeyRaw = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ValueRaw = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportedTraceProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportedTraceProperties_ImportedTraces_ImportedTraceId",
                        column: x => x.ImportedTraceId,
                        principalTable: "ImportedTraces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportBatches_FileHash",
                table: "ImportBatches",
                column: "FileHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportedTraceProperties_ImportedTraceId_Position",
                table: "ImportedTraceProperties",
                columns: new[] { "ImportedTraceId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportedTraces_ImportSourceFileId_Position",
                table: "ImportedTraces",
                columns: new[] { "ImportSourceFileId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportedTraces_TraceHash",
                table: "ImportedTraces",
                column: "TraceHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportErrors_ImportBatchId",
                table: "ImportErrors",
                column: "ImportBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportErrors_ImportSourceFileId",
                table: "ImportErrors",
                column: "ImportSourceFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportSourceFiles_ContentHash",
                table: "ImportSourceFiles",
                column: "ContentHash");

            migrationBuilder.CreateIndex(
                name: "IX_ImportSourceFiles_ImportBatchId",
                table: "ImportSourceFiles",
                column: "ImportBatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportedTraceProperties");

            migrationBuilder.DropTable(
                name: "ImportErrors");

            migrationBuilder.DropTable(
                name: "ImportedTraces");

            migrationBuilder.DropTable(
                name: "ImportSourceFiles");

            migrationBuilder.DropTable(
                name: "ImportBatches");
        }
    }
}
