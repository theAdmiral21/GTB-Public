using Primitives.Common.Diagnostics;
using Primitives.Common.Diagnostics.DataStructures;
using Primitives.Common.Diagnostics.Enums;
using UnityEngine;

namespace Diagnostics.Unity
{
    public class UnityDiagnosticSink : IDiagnosticSink
    {
        public void Emit(in DiagnosticEvent diagnostic)
        {
            switch (diagnostic.Level)
            {
                case LogLevel.Warning:
                    {
                        Debug.LogWarning(diagnostic.Message);
                        break;
                    }
                case LogLevel.Fatal:
                case LogLevel.Error:
                    {
                        Debug.LogError(diagnostic.Message);
                        break;
                    }
                default:
                    {
                        Debug.Log(diagnostic.Message);
                        break;
                    }
            }
        }
    }
}