using System;
using System.Collections.Generic;

namespace Domain.Imports
{
    public class ImportSourceFile
    {
        private readonly List<ImportedTrace> _traces = new();
        private readonly List<ImportError> _errors = new();

        private ImportSourceFile()
        {
        }

        public ImportSourceFile(string originalFileName, long fileSizeBytes, string? contentHash)
        {
            if (string.IsNullOrWhiteSpace(originalFileName))
            {
                throw new ArgumentException("File name is required.", nameof(originalFileName));
            }

            if (fileSizeBytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fileSizeBytes), "File size cannot be negative.");
            }

            OriginalFileName = originalFileName.Trim();
            FileSizeBytes = fileSizeBytes;
            ContentHash = NormalizeHash(contentHash);
            Status = ImportSourceFileStatus.Processing;
        }

        public int Id { get; private set; }

        public int ImportBatchId { get; private set; }

        public string OriginalFileName { get; private set; } = string.Empty;

        public long FileSizeBytes { get; private set; }

        /*
         * L'empreinte est nullable car certaines entrées peuvent être rejetées
         * avant que leur contenu soit lu : chemin ZIP dangereux, format refusé,
         * limite de taille dépassée, etc.
         */
        public string? ContentHash { get; private set; }

        public ImportSourceFileStatus Status { get; private set; }

        public int ImportedTraceCount { get; private set; }

        public int RejectedTraceCount { get; private set; }

        public int SkippedTraceCount { get; private set; }

        public string? TechnicalMessage { get; private set; }

        public ImportBatch ImportBatch { get; private set; } = null!;

        public IReadOnlyCollection<ImportedTrace> Traces
            => _traces.AsReadOnly();

        public IReadOnlyCollection<ImportError> Errors
            => _errors.AsReadOnly();

        internal void AttachToBatch(ImportBatch importBatch)
        {
            ArgumentNullException.ThrowIfNull(importBatch);

            if (ImportBatch is not null && !ReferenceEquals(ImportBatch, importBatch))
            {
                throw new InvalidOperationException("Source file is already attached to a different batch.");
            }

            ImportBatch = importBatch;
        }

        public void AddTrace(ImportedTrace importedTrace)
        {
            ArgumentNullException.ThrowIfNull(importedTrace);

            EnsureProcessing();

            if (_traces.Contains(importedTrace))
            {
                throw new InvalidOperationException("This trace is already attached to the source file.");
            }

            _traces.Add(importedTrace);
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

        public void Complete(int rejectedTraceCount, int skippedTraceCount)
        {
            FinalizeImport(
                ImportSourceFileStatus.Completed,
                rejectedTraceCount,
                skippedTraceCount,
                technicalMessage: null);
        }

        public void CompleteWithErrors(int rejectedTraceCount, int skippedTraceCount, string? technicalMessage = null)
        {
            FinalizeImport(
                ImportSourceFileStatus.CompletedWithErrors,
                rejectedTraceCount,
                skippedTraceCount,
                technicalMessage);
        }

        public void MarkAsDuplicate(string technicalMessage)
        {
            EnsureProcessing();

            if (string.IsNullOrWhiteSpace(technicalMessage))
            {
                throw new ArgumentException("Technical message is required for a duplicate file.", nameof(technicalMessage));
            }

            ImportedTraceCount = 0;
            RejectedTraceCount = 0;
            SkippedTraceCount = 0;
            TechnicalMessage = technicalMessage;
            Status = ImportSourceFileStatus.Duplicate;
        }

        public void Fail(string technicalMessage, int rejectedTraceCount = 0)
        {
            EnsureProcessing();

            if (string.IsNullOrWhiteSpace(technicalMessage))
            {
                throw new ArgumentException("Technical message is required for a failed file.", nameof(technicalMessage));
            }

            ValidateCounter(
                rejectedTraceCount,
                nameof(rejectedTraceCount));

            ImportedTraceCount = _traces.Count;
            RejectedTraceCount = rejectedTraceCount;
            SkippedTraceCount = 0;
            TechnicalMessage = technicalMessage;
            Status = ImportSourceFileStatus.Failed;
        }

        private void FinalizeImport(ImportSourceFileStatus finalStatus, int rejectedTraceCount, int skippedTraceCount, string? technicalMessage)
        {
            EnsureProcessing();

            ValidateCounter(
                rejectedTraceCount,
                nameof(rejectedTraceCount));

            ValidateCounter(
                skippedTraceCount,
                nameof(skippedTraceCount));

            ImportedTraceCount = _traces.Count;
            RejectedTraceCount = rejectedTraceCount;
            SkippedTraceCount = skippedTraceCount;
            TechnicalMessage = technicalMessage;
            Status = finalStatus;
        }

        private void EnsureProcessing()
        {
            if (Status != ImportSourceFileStatus.Processing)
            {
                throw new InvalidOperationException("This operation requires a processing file.");
            }
        }

        private static void ValidateCounter(int counter, string parameterName)
        {
            if (counter < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName,"A counter cannot be negative.");
            }
        }

        private static string? NormalizeHash(string? contentHash)
        {
            if (string.IsNullOrWhiteSpace(contentHash))
            {
                return null;
            }

            return contentHash.Trim().ToLowerInvariant();
        }
    }
}