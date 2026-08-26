using BL.Mappers.Imports;
using BL.Models.Imports;
using Domain.Imports;

namespace BL.Tests.Mappers.Imports
{
    public class ImportErrorMapperTests
    {
        private readonly ImportErrorMapper _mapper = new();

        [Fact]
        public void Map_ShouldCreateAndAttachFileError()
        {
            DateTime batchCreatedAtUtc = new(
                2026,
                8,
                26,
                8,
                0,
                0,
                DateTimeKind.Utc);

            DateTime errorCreatedAtUtc =
                batchCreatedAtUtc.AddMinutes(1);

            ImportBatch importBatch = new(
                "Import.xml",
                1000,
                new string('a', 64),
                batchCreatedAtUtc);

            importBatch.Start(
                batchCreatedAtUtc.AddSeconds(1));

            ImportSourceFile sourceFile = new(
                "traces.xml",
                900,
                new string('b', 64));

            importBatch.AddSourceFile(sourceFile);

            XmlTraceParseError parseError = new()
            {
                Code = "technical_time_invalid",
                Message = "The technical time is invalid.",
                TracePosition = 3,
                Severity = ImportErrorSeverity.Warning,
                RejectsTrace = false
            };

            ImportError importError = _mapper.Map(
                parseError,
                sourceFile.OriginalFileName,
                errorCreatedAtUtc);

            importBatch.AddError(
                importError,
                sourceFile);

            Assert.Equal(
                "TECHNICAL_TIME_INVALID",
                importError.Code);

            Assert.Equal(
                "The technical time is invalid.",
                importError.Message);

            Assert.Equal(
                ImportErrorSeverity.Warning,
                importError.Severity);

            Assert.Equal(
                "traces.xml",
                importError.FileName);

            Assert.Equal(
                3,
                importError.TracePosition);

            Assert.Equal(
                errorCreatedAtUtc,
                importError.CreatedAtUtc);

            Assert.Same(
                importBatch,
                importError.ImportBatch);

            Assert.Same(
                sourceFile,
                importError.ImportSourceFile);

            Assert.Same(
                importError,
                Assert.Single(importBatch.Errors));

            Assert.Same(
                importError,
                Assert.Single(sourceFile.Errors));
        }

        [Fact]
        public void AddError_ShouldSupportBatchLevelError()
        {
            DateTime createdAtUtc = new(
                2026,
                8,
                26,
                8,
                0,
                0,
                DateTimeKind.Utc);

            ImportBatch importBatch = new(
                "Import.zip",
                1000,
                new string('a', 64),
                createdAtUtc);

            ImportError importError = new(
                "ZIP_INVALID",
                "The ZIP archive is invalid.",
                ImportErrorSeverity.Error,
                createdAtUtc);

            importBatch.AddError(importError);

            Assert.Same(
                importBatch,
                importError.ImportBatch);

            Assert.Null(importError.ImportSourceFile);
            Assert.Null(importError.ImportSourceFileId);

            Assert.Same(
                importError,
                Assert.Single(importBatch.Errors));
        }

        [Fact]
        public void AddError_ShouldRejectFileFromAnotherBatch()
        {
            DateTime createdAtUtc = new(
                2026,
                8,
                26,
                8,
                0,
                0,
                DateTimeKind.Utc);

            ImportBatch firstBatch = new(
                "First.xml",
                1000,
                new string('a', 64),
                createdAtUtc);

            ImportBatch secondBatch = new(
                "Second.xml",
                1000,
                new string('b', 64),
                createdAtUtc);

            firstBatch.Start(
                createdAtUtc.AddSeconds(1));

            secondBatch.Start(
                createdAtUtc.AddSeconds(1));

            ImportSourceFile sourceFile = new(
                "traces.xml",
                900,
                new string('c', 64));

            firstBatch.AddSourceFile(sourceFile);

            ImportError importError = new(
                "XML_INVALID",
                "The XML document is invalid.",
                ImportErrorSeverity.Error,
                createdAtUtc.AddMinutes(1),
                sourceFile.OriginalFileName);

            Assert.Throws<InvalidOperationException>(
                () => secondBatch.AddError(
                    importError,
                    sourceFile));

            Assert.Empty(secondBatch.Errors);
            Assert.Empty(sourceFile.Errors);
        }

        [Fact]
        public void Constructor_ShouldRejectNonUtcDate()
        {
            DateTime localDate = new(
                2026,
                8,
                26,
                8,
                0,
                0,
                DateTimeKind.Local);

            Assert.Throws<ArgumentException>(
                () => new ImportError(
                    "XML_INVALID",
                    "The XML document is invalid.",
                    ImportErrorSeverity.Error,
                    localDate));
        }
    }
}