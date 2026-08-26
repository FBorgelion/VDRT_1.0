using BL.Models.Imports;
using Domain.Imports;

namespace BL.Mappers.Imports
{
    public sealed class ImportErrorMapper
    {
        public ImportError Map(XmlTraceParseError parseError, string fileName, DateTime createdAtUtc)
        {
            ArgumentNullException.ThrowIfNull(parseError);

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("The source file name is required.", nameof(fileName));
            }

            return new ImportError(
                parseError.Code,
                parseError.Message,
                parseError.Severity,
                createdAtUtc,
                fileName,
                parseError.TracePosition);
        }
    }
}