namespace Domain.Activities
{
    public static class ActivityTraceTypes
    {
        public const int MidnightMarker = 9;
        public const int Opening = 10;
        public const int Closing = 11;
        public const int Information = 12;
        public const int ValidatedClosing = 13;

        public static bool IsActivityTrace(int traceType)
        {
            return traceType == MidnightMarker
                || traceType == Opening
                || traceType == Closing
                || traceType == Information
                || traceType == ValidatedClosing;
        }

        public static bool IsClosingTrace(int traceType)
        {
            return traceType == Closing
                || traceType == ValidatedClosing;
        }
    }
}