using Autofac;
using ESFA.DC.Logging;
using ESFA.DC.Logging.SeriLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = ConfigureBuilder();
            var container = builder.Build();
            var scope = container.BeginLifetimeScope();
            //using (var scope = container.BeginLifetimeScope())
            //{
              var logger = container.Resolve<ILogger>();


                logger.LogDebug("some debug");
                logger.LogInfo("some debug");
                logger.LogWarning("some debug");
                logger.LogError("some debug",null);
                logger.LogInfo("test info {@builder}",builder);
                //logger.LogWarning("test warn");
                logger.LogError("test error data {@container}",new Exception("exception occured"), container);
            //}

            //Thread.Sleep(1000);
            Console.ReadLine();

        }

        private static ContainerBuilder ConfigureBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<LoggerModule>();


            return builder;
        }
    }
}
