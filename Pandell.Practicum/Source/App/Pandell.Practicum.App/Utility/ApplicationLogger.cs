using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Utility
{
    public static class ApplicationLogger
    {
        private static object LockSyncRoot;
        private static ILog Log;

        static ApplicationLogger()
        {
            if (Log != null && LoggerRepository != null) return;
            
            Initialize();
            LoadXmlConfiguration();
        }

        public static async Task LogInformationAsync(string message)
        {
            await LogMessageAsync(message, LogInformation).ConfigureAwait(false);
        }

        public static async Task LogWarningAsync(string message)
        {
            await LogMessageAsync(message, LogWarning).ConfigureAwait(false);
        }

        public static async Task LogExceptionAsync(string message, Exception exception)
        {
            await Task.Run(() =>
                {
                    lock (LockSyncRoot)
                    {
                        Log.Error(message, exception);
                    }
                }).ConfigureAwait(false);
        }
        
        private static async Task LogMessageAsync(string message, Action<string> logMessageTypeAction)
        {
            await Task.Run(() => logMessageTypeAction(message)).ConfigureAwait(false);
        }

        private static void LogInformation(string message)
        {
            lock (LockSyncRoot)
            {
                Log.Info(message);        
            }
        }

        private static void LogWarning(string message)
        {
            lock (LockSyncRoot)
            {
                Log.Warn(message);
            }
        }
        
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private static void Initialize()
        {
            LockSyncRoot = new object();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            LoggerRepository = LogManager.CreateRepository(Assembly.GetExecutingAssembly(), typeof(Hierarchy));
        }

        private static void LoadXmlConfiguration()
        {
            var logFileConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, 
                ApplicationConfigurationCodes.Log4NetConfigFile.ToDescription());
            
            XmlConfigurator.Configure(LoggerRepository, new FileInfo(logFileConfigPath));
        }
        
        #region Class Members
        
        public static ILoggerRepository LoggerRepository { get; set; }

        #endregion
    }
}