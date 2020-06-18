using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Pandell.Practicum.App.Utility;
using Pandell.Practicum.UnitTests.Utility.Abstraction;
using Xunit;

namespace Pandell.Practicum.UnitTests.Utility
{
    [ExcludeFromCodeCoverage]
    public class ApplicationLoggerTest : IDisposable
    {
        private const int millisecondDelay = 1000;
        private const int retryAttempts = 2;
        
        private readonly IAssertApplicationLogger assertApplicationLogger;

        public ApplicationLoggerTest()
        {
            assertApplicationLogger = new AssertApplicationLogger();
            AssertClearOutAllLogFileContents();
        }

        public void Dispose()
        {
            AssertClearOutAllLogFileContents();
        }

        [Fact]
        public async Task LogInformationAsync_LogsAnInformationMessage_ToTheLogFile()
        {
            await ExecuteLogFileActionAsync("This is an information message",
                ApplicationLogger.LogInformationAsync)
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task LogWarningAsync_LogsAWarningMessage_ToTheLogFile()
        {
            await ExecuteLogFileActionAsync("This is an warning message",
                ApplicationLogger.LogWarningAsync)
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task LogExceptionAsync_LogsAnExceptionMessage_ToTheLogFile()
        {
            const string exceptionMessage = "This is an exception message";
            var testException = new Exception("This is an exception message");
            
            await ApplicationLogger.LogExceptionAsync(testException.Message, testException).ConfigureAwait(false);    
            assertApplicationLogger.AssertMessageIsInLogFile(exceptionMessage, retryAttempts, millisecondDelay);
        }
        
        private async Task ExecuteLogFileActionAsync(string message, Func<string, Task> logFileAction)
        {
            await logFileAction(message).ConfigureAwait(false);
            assertApplicationLogger.AssertMessageIsInLogFile(message, retryAttempts, millisecondDelay);
        }

        private void AssertClearOutAllLogFileContents()
        {
            assertApplicationLogger.DeleteAllLogFileContents(retryAttempts, millisecondDelay);
            var logFileContents = assertApplicationLogger.GetLogFileContents(retryAttempts, millisecondDelay);
            logFileContents.Should().BeEmpty();
        }
    }
}