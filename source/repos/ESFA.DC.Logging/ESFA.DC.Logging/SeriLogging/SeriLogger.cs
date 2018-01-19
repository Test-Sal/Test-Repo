﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using Serilog.Events;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Context;
using System.Diagnostics;

namespace ESFA.DC.Logging.SeriLogging
{
    public class SeriLogger : ILogger
    {
        private Logger logger = null;
        private ApplicationLoggerSettings _appLoggerSettings = null;
        private LoggerConfiguration _seriConfig = null;
        

        private ILogEventSink _sink = null;

        #region Constructors
    
        public SeriLogger(ApplicationLoggerSettings appConfig)
        {
            _appLoggerSettings = appConfig;
            _seriConfig = ConfigureSerilog();

            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            //Serilog.Debugging.SelfLog.Enable(Console.Error);

            if (appConfig.LoggerOutput == Enums.LogOutputDestination.SqlServer)
            {
              logger =   SqlServerLoggerFactory.CreateLogger(_seriConfig, appConfig.ConnectionStringKey, appConfig.LogsTableName);
            }
            else if (appConfig.LoggerOutput == Enums.LogOutputDestination.Console)
            {
               logger= ConsoleLoggerFactory.CreateLogger(_seriConfig);
            }
               
        }
        public SeriLogger(ApplicationLoggerSettings appConfig, ILogEventSink sink)
        {
            _appLoggerSettings = appConfig;
            _sink = sink;
            logger = GenericLoggerFactory.CreateLogger(_seriConfig,sink);
        }


        #endregion

        #region Public Methods
       

        /// <summary>
        /// Creates the logger configuration for serilog
        /// </summary>
        /// <param name="appConfig"></param>
        /// <returns></returns>
        public LoggerConfiguration ConfigureSerilog()
        {
            
            //setup the configuartion
            var seriConfig = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithProcessName()
                    .Enrich.WithThreadId()
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName() // not sure if this will be useful??
                    .Enrich.WithProperty("ApplicationId", _appLoggerSettings.ApplicationName);
                    
                    
            return seriConfig;
        }

       
        #endregion

        #region Logger functions
        public void LogError(string message, Exception ex, params object[] parameters )
        {
            logger.Error(ex,message,parameters);
        }
        
        public void LogWarning(string message, params object[] parameters)
        {
            logger.Warning(message,parameters);
        }

        public void LogDebug(string message, params object[] parameters)
        {
            logger.Debug(message,parameters);
        }

        public void LogInfo(string message,params object[] parameters)
        {
            logger.Information(message, parameters);
        }

        #endregion

    }
}