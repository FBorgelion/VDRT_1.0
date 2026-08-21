using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public sealed class RawActivityTrace
    {
        public required string ImportFingerprint { get; init; }

        public required int PositionInFile { get; init; }

        // LID
        public required string ExternalActivityId { get; init; }


        public string RawSourceReference { get; init; } = string.Empty;

        // type XML : 9, 10, 11, 12, 13...
        public required int RawTraceType { get; init; }

        // time
        public required string RawTraceTime { get; init; }

        /// <summary>
        /// Horodatage technique analysé.
        /// Tant que le fuseau n'est pas confirmé, utiliser DateTimeKind.Unspecified.
        /// </summary>
        public DateTime? TraceTime { get; init; }

        // SEQ
        public long? ExternalSequenceNumber { get; init; }

        // ATY
        public string? RawActivityCode { get; init; }

        // AST
        public string? RawActivityStartTime { get; init; }

        public DateTime? ActivityStartTime { get; init; }

        // ALEN
        public long? DurationMilliseconds { get; init; }

        // DID
        public string? RawExternalDriverIds { get; init; }

        // ARE
        public string? RawActivityReport { get; init; }

        // AFRE
        public string? RawFinalActivityReport { get; init; }

        public required string RawXml { get; init; }
    }
}

