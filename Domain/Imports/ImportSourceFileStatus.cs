namespace Domain.Imports
{
    public enum ImportSourceFileStatus
    {
        Processing = 0,
        Completed = 1,
        CompletedWithErrors = 2,
        Failed = 3,
        Duplicate = 4
    }
}