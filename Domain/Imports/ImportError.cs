using System;
using System.Linq;

namespace Domain.Imports
{
    public class ImportError
    {
        private const int MaximumCodeLength = 100;
        private const int MaximumMessageLength = 2000;
        private const int MaximumFileNameLength = 500;

        private ImportError()
        {
        }

        public ImportError(string code, string message, ImportErrorSeverity severity, DateTime createdAtUtc, string? fileName = null, int? tracePosition = null)
        {
            string normalizedCode = NormalizeRequiredText(
                code,
                MaximumCodeLength,
                nameof(code))
                .ToUpperInvariant();

            bool containsInvalidCodeCharacter = normalizedCode.Any(character => !IsValidCodeCharacter(character));

            if (containsInvalidCodeCharacter)
            {
                throw new ArgumentException( "The error code can only contain uppercase " + "letters, digits and underscores.", nameof(code));
            }

            if (!Enum.IsDefined(typeof(ImportErrorSeverity), severity))
            {
                throw new ArgumentOutOfRangeException(nameof(severity),"The error severity is invalid.");
            }

            EnsureUtcDate(createdAtUtc, nameof(createdAtUtc));

            if (tracePosition.HasValue && tracePosition.Value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tracePosition), "The trace position must be greater than zero.");
            }

            Code = normalizedCode;

            Message = NormalizeRequiredText(message, MaximumMessageLength, nameof(message));

            FileName = NormalizeOptionalText(fileName, MaximumFileNameLength, nameof(fileName));

            TracePosition = tracePosition;
            Severity = severity;
            CreatedAtUtc = createdAtUtc;
        }

        public int Id { get; private set; }

        public int ImportBatchId { get; private set; }

        public int? ImportSourceFileId { get; private set; }

        public string Code { get; private set; } = string.Empty;

        public string Message { get; private set; } = string.Empty;

        public string? FileName { get; private set; }

        public int? TracePosition { get; private set; }

        public ImportErrorSeverity Severity { get; private set; }

        public DateTime CreatedAtUtc { get; private set; }

        public ImportBatch ImportBatch
        {
            get;
            private set;
        } = null!;

        public ImportSourceFile? ImportSourceFile
        {
            get;
            private set;
        }

        internal void AttachTo(ImportBatch importBatch, ImportSourceFile? sourceFile)
        {
            ArgumentNullException.ThrowIfNull(importBatch);

            if (ImportBatch is not null)
            {
                bool alreadyCorrectlyAttached = ReferenceEquals(ImportBatch, importBatch) && ReferenceEquals(ImportSourceFile, sourceFile);

                if (alreadyCorrectlyAttached)
                {
                    return;
                }

                throw new InvalidOperationException("The error is already attached " + "to another import context.");
            }

            if (sourceFile is not null && !ReferenceEquals(sourceFile.ImportBatch, importBatch))
            {
                throw new InvalidOperationException("The source file does not belong " + "to the specified import batch.");
            }

            if (CreatedAtUtc < importBatch.CreatedAtUtc)
            {
                throw new InvalidOperationException("The error date cannot precede " + "the batch creation date.");
            }

            ImportBatch = importBatch;
            ImportSourceFile = sourceFile;
        }

        private static string NormalizeRequiredText(string value, int maximumLength, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The value is required.", parameterName);
            }

            string normalizedValue = value.Trim();

            if (normalizedValue.Length > maximumLength)
            {
                throw new ArgumentException($"The value cannot exceed " + $"{maximumLength} characters.", parameterName);
            }

            return normalizedValue;
        }

        private static string? NormalizeOptionalText(string? value, int maximumLength, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            string normalizedValue = value.Trim();

            if (normalizedValue.Length > maximumLength)
            {
                throw new ArgumentException($"The value cannot exceed " + $"{maximumLength} characters.", parameterName);
            }

            return normalizedValue;
        }

        private static bool IsValidCodeCharacter(char character)
        {
            return character >= 'A' && character <= 'Z' || character >= '0' && character <= '9' || character == '_';
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