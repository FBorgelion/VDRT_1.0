using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Activities.Interfaces
{
    public interface IActivityCodeMapper
    {
        ActivityKind Map(string? rawCode);
    }
}
