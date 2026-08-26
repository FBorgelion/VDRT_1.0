using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Imports
{
    public class ImportBatch
    {
        private readonly List<ImportSourceFile> _sourceFiles = new();
        private readonly List<ImportError> _errors = new();

        private ImportBatch()
        {
        }

        public ImportBatch(string originalFileName, long originalFileSizeBytes, string fileHash, DateTime createdAtUtc)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentException("File name is required.", nameof(originalFileName));
            }

            if (originalFileSizeBytes <= 0)
            {
                throw new ArgumentOutOfRangeException( nameof(originalFileSizeBytes), "The file size must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(fileHash))
            {
                throw new ArgumentException( "File hash is required.", nameof(fileHash));
            }

            EnsureUtcDate(createdAtUtc, nameof(createdAtUtc));

            OriginalFileName = originalFileName.Trim();
            OriginalFileSizeBytes = originalFileSizeBytes;
            FileHash = fileHash.Trim().ToLowerInvariant();
            CreatedAtUtc = createdAtUtc;
            Status = ImportBatchStatus.Pending;
        }

        public int Id { get; private set; }

        public string OriginalFileName { get; private set; } = string.Empty;

        public long OriginalFileSizeBytes { get; private set; }

        public string FileHash { get; private set; } = string.Empty;

        public ImportBatchStatus Status { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public DateTime? StartedAtUtc { get; private set; }

        public DateTime? CompletedAtUtc { get; private set; }

        public int TotalFiles { get; private set; }

        public int SuccessfulFiles { get; private set; }

        public int FailedFiles { get; private set; }

        public int ImportedTraceCount { get; private set; }

        public int RejectedTraceCount { get; private set; }

        public int SkippedTraceCount { get; private set; }

        public string? TechnicalMessage { get; private set; }

        public IReadOnlyCollection<ImportSourceFile> SourceFiles => _sourceFiles.AsReadOnly();

        public IReadOnlyCollection<ImportError> Errors => _errors.AsReadOnly();

        public void Start(DateTime startedAtUtc)
        {
            if (Status != ImportBatchStatus.Pending)
            {
                throw new InvalidOperationException("Only a pending batch can be started.");
            }

            EnsureUtcDate(startedAtUtc, nameof(startedAtUtc));

            if (startedAtUtc < CreatedAtUtc)
            {
                throw new ArgumentException(
                    "The start date cannot precede the batch creation date.",
                    nameof(startedAtUtc));
            }

            StartedAtUtc = startedAtUtc;
            Status = ImportBatchStatus.Processing;
        }

        public void AddSourceFile(ImportSourceFile sourceFile)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);

            if (Status != ImportBatchStatus.Processing)
            {
                throw new InvalidOperationException("Only a processing batch can have source files added.");
            }

            if (_sourceFiles.Contains(sourceFile))
            {
                throw new InvalidOperationException("This source file already belongs to the batch.");
            }

            sourceFile.AttachToBatch(this);
            _sourceFiles.Add(sourceFile);
        }

        public void AddError(ImportError importError)
        {
            ArgumentNullException.ThrowIfNull(importError);

            if (_errors.Contains(importError))
            {
                return;
            }

            _errors.Add(importError);
        }

        public void Complete(DateTime completedAtUtc)
        {
            if (Status != ImportBatchStatus.Processing)
            {
                throw new InvalidOperationException("Only a processing batch can be completed.");
            }

            ValidateCompletionDate(completedAtUtc);
            RecalculateCounters();

            CompletedAtUtc = completedAtUtc;

            bool containsFileWithErrors = _sourceFiles.Any(sourceFile => sourceFile.Status == ImportSourceFileStatus.CompletedWithErrors);

            if (TotalFiles == 0)
            {
                Status = ImportBatchStatus.Failed;
                TechnicalMessage = "No valid XML files were found.";
                return;
            }

            if (SuccessfulFiles == 0)
            {
                Status = ImportBatchStatus.Failed;
                TechnicalMessage = "No valid XML files were found.";
                return;
            }

            if (FailedFiles > 0 || containsFileWithErrors)
            {
                Status = ImportBatchStatus.CompletedWithErrors;
                return;
            }

            Status = ImportBatchStatus.Completed;
        }

        public void Fail(string technicalMessage, DateTime completedAtUtc)
        {
            if (Status != ImportBatchStatus.Pending && Status != ImportBatchStatus.Processing)
            {
                throw new InvalidOperationException("Only a pending or processing batch can be failed.");
            }

            if (string.IsNullOrWhiteSpace(technicalMessage))
            {
                throw new ArgumentException("A technical message is required for a failed batch.", nameof(technicalMessage));
            }

            EnsureUtcDate(completedAtUtc, nameof(completedAtUtc));

            if (completedAtUtc < CreatedAtUtc)
            {
                throw new ArgumentException("The completion date cannot precede the batch creation date.", nameof(completedAtUtc));
            }

            RecalculateCounters();

            TechnicalMessage = technicalMessage;
            CompletedAtUtc = completedAtUtc;
            Status = ImportBatchStatus.Failed;
        }

        private void RecalculateCounters()
        {
            TotalFiles = _sourceFiles.Count;

            SuccessfulFiles = _sourceFiles.Count(sourceFile =>
                sourceFile.Status == ImportSourceFileStatus.Completed
                || sourceFile.Status == ImportSourceFileStatus.CompletedWithErrors
                || sourceFile.Status == ImportSourceFileStatus.Duplicate);

            FailedFiles = _sourceFiles.Count(sourceFile =>
                sourceFile.Status == ImportSourceFileStatus.Failed);

            ImportedTraceCount = _sourceFiles.Sum(sourceFile =>
                sourceFile.ImportedTraceCount);

            RejectedTraceCount = _sourceFiles.Sum(sourceFile =>
                sourceFile.RejectedTraceCount);

            SkippedTraceCount = _sourceFiles.Sum(sourceFile =>
                sourceFile.SkippedTraceCount);
        }

        private void ValidateCompletionDate(DateTime completedAtUtc)
        {
            EnsureUtcDate(completedAtUtc, nameof(completedAtUtc));

            if (StartedAtUtc.HasValue && completedAtUtc < StartedAtUtc.Value)
            {
                throw new ArgumentException( "The completion date cannot precede the batch start date.", nameof(completedAtUtc));
            }
        }

        private static void EnsureUtcDate(DateTime date, string parameterName)
        {
            if (date.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("The date must be expressed in UTC.", parameterName);
            }
        }
    }
}