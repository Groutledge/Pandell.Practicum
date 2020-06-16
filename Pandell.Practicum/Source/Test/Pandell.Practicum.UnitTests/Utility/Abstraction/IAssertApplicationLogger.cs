using System;
using System.IO;
using System.Linq;
using System.Threading;
using FluentAssertions;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.UnitTests.Utility.Abstraction
{
    public interface IAssertApplicationLogger
    {
        public void AssertMessageIsInLogFile(string message, 
            int retryAttempts, 
            int millisecondDelay)
        {
            var allLogFileContents = GetLogFileContents(retryAttempts, millisecondDelay);
            allLogFileContents.Contains(message).Should().BeTrue();
        }

        public string GetLogFileContents(int retryAttempts, int millisecondDelay)
        {
            var rollingFileAppender = ((Hierarchy) ApplicationLogger.LoggerRepository).Root.Appenders.OfType<RollingFileAppender>().FirstOrDefault();
            rollingFileAppender.Should().NotBeNull();
            
            var allLogFileContents = GetAllLogFileContents(rollingFileAppender, 
                retryAttempts, 
                millisecondDelay);
            
            return allLogFileContents;
        }

        public void DeleteAllLogFileContents(int retryAttempts, int millisecondDelay)
        {
            var rollingFileAppender = ((Hierarchy) ApplicationLogger.LoggerRepository).Root.Appenders.OfType<RollingFileAppender>().First();
            var canAccessLogFile = false;

            for (int i = 0; i < retryAttempts; i++)
            {
                try
                {
                    File.WriteAllText(rollingFileAppender.File, string.Empty);
                    canAccessLogFile = true;
                }
                catch (Exception)
                {
                    Thread.Sleep(millisecondDelay);
                }   
                
                if (canAccessLogFile) break;
            }
        }
        
        private string GetAllLogFileContents(FileAppender rollingFileAppender, 
            int retryAttempts, 
            int millisecondDelay)
        {
            string allLogFileContents = null;
            var canAccessLogFile = false;

            for (int i = 0; i < retryAttempts; i++)
            {
                try
                {
                    allLogFileContents = File.ReadAllText(rollingFileAppender.File);
                    canAccessLogFile = true;
                }
                catch (IOException)
                {
                    Thread.Sleep(millisecondDelay);
                }

                if (canAccessLogFile) break;
            }

            return allLogFileContents;
        }
    }
}