using System.Collections.Generic;
using Primitives.Common.Diagnostics.Enums;

namespace Primitives.Common.Diagnostics.DataStructures
{
    public readonly struct DiagnosticEvent
    {
        public LogLevel Level { get; }
        public string Message { get; }
        public string Source { get; }
        public IReadOnlyDictionary<string, object> Data { get; }

        public DiagnosticEvent(
            LogLevel level,
            string message,
            string source,
            IReadOnlyDictionary<string, object> data
        )
        {
            Level = level;
            Message = message;
            Source = source;
            Data = data;
        }
    }
}