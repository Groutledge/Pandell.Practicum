using System.IO;
using Microsoft.Extensions.Configuration;
using Pandell.Practicum.App.Enumerations;
using Pandell.Practicum.App.Extensions;

namespace Pandell.Practicum.App.Configuration
{
    public static class ConfigurationFile
    {
        private static readonly string ApplicationSettingsFileName = ApplicationConfigurationCodes.JsonAppSettings.ToDescription();
        private static IConfiguration Configuration;
        
        public static string DefaultConnectionString => GetConnectionString(ApplicationConfigurationCodes.DefaultDbConnection.ToDescription());

        static ConfigurationFile()
        {
            if (Configuration != null) return;
            CreateLinkToConfigurationFile();
        }

        private static void CreateLinkToConfigurationFile()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ApplicationSettingsFileName, true, true);
            
            Configuration = configurationBuilder.Build();
        }
        
        private static string GetConnectionString(string connectionStringName)
        {
            return Configuration.GetSection(string.Concat(ApplicationConfigurationCodes.ConnectionStrings.ToDescription(), connectionStringName)).Value;
        }
    }
}