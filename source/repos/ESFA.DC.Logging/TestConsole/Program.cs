using ESFA.DC.Logging;
using ESFA.DC.Logging.SeriLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ApplicationLoggerSettings();
            config.ConnectionStringKey = "AuditLoggingConnectionString";
            config.LoggerOutput = ESFA.DC.Logging.Enums.LogOutputDestination.SqlServer;


            ILogger logger = new SeriLogger(config);

            logger.LogError("test",null);
            Console.ReadLine();

        }
    }
}
