using BL.Models.Imports;

namespace BL.Interfaces.Services
{
    public interface IXmlTraceParser
    {
        Task<XmlTraceParseResult> ParseAsync(Stream xmlStream, CancellationToken cancellationToken);
    }
}