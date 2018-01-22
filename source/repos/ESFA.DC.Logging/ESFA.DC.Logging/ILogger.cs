using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.Logging
{
    public interface ILogger :IDisposable
    {
        void LogError(string message, Exception ex, params object[] parameters);
        void LogWarning(string message, params object[] parameters);
        void LogDebug(string message, params object[] parameters);
        void LogInfo(string message, params object[] parameters);
        
    }
}
