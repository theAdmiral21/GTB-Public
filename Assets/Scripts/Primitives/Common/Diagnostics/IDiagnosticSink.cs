using Primitives.Common.Diagnostics.DataStructures;

namespace Primitives.Common.Diagnostics
{
    public interface IDiagnosticSink
    {
        public void Emit(in DiagnosticEvent diagnostic);
    }
}