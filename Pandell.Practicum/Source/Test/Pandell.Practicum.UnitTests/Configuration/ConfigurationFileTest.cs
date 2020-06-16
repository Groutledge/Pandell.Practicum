using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Pandell.Practicum.App.Configuration;
using Xunit;

namespace Pandell.Practicum.UnitTests.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ConfigurationFileTest
    {
        [Fact]
        public void DefaultConnection_ReturnsTheDefaultDatabaseConnectionString_FromTheConfigurationFile()
        {
            (string ExpectedDatabaseName, string ExpectedSslMode) expectedDatabaseValues = GetExpectedDatabaseConnectionStringValues();
            
            var defaultConnectionString = ConfigurationFile.DefaultConnectionString;
            
            defaultConnectionString.Should()
                .NotBeEmpty()
                .And.Contain(expectedDatabaseValues.ExpectedDatabaseName)
                .And.Contain(expectedDatabaseValues.ExpectedSslMode);
        }

        private (string ExpectedDatabase, string ExpectedSslMode) GetExpectedDatabaseConnectionStringValues()
        {
            return ("database=Pandell;", "SslMode=Required;");
        }
    }
}