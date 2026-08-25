using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities
{
    public class RawActivityTrace
    {
        public int TraceType { get; init; } //type

        public string SourceId { get; init; } = string.Empty;

        public DateTime TechnicalTime { get; init; }

        public string? LinkId { get; init; } //LID

        public string? ActivityCode { get; init; } //ATY

        public string? DriverId { get; init; } //DID

        public long? Sequence { get; init; } //SEQ

        public DateTime? ActivityStartTime { get; init; } //AST

        public long? ActivityLengthMilliseconds { get; init; } //ALEN
    }
}

