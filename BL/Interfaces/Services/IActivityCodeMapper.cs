using Domain.Activities;

namespace BL.Interfaces.Services
{
    public interface IActivityCodeMapper
    {
        ActivityKind Map(string? rawActivityCode);
    }
}
