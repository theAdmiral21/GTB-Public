using System.Collections.Generic;
using System.Linq;
using Primitives.Common.Diagnostics;
using Primitives.Common.Diagnostics.DataStructures;

namespace Diagnostics.Application
{
    public sealed class DiagnosticServce : IDiagnosticSink
    {
        private readonly IReadOnlyList<IDiagnosticSink> _sinks;
        public DiagnosticServce(IEnumerable<IDiagnosticSink> sinks)
        {
            _sinks = sinks.ToList();
        }
        public void Emit(in DiagnosticEvent diagnostic)
        {
            foreach (var sink in _sinks)
            {
                sink.Emit(diagnostic);
            }
        }
    }
}