namespace Domain.Imports
{
    public enum ImportBatchStatus
    {
        Pending = 0,
        Processing = 1,
        Completed = 2,
        CompletedWithErrors = 3,
        Failed = 4
    }
}