using System.ComponentModel;

namespace Pandell.Practicum.App.Enumerations
{
    public enum ApplicationConfigurationCodes
    {
        [Description("appsettings.json")] JsonAppSettings = 0,
        [Description("ConnectionStrings:")] ConnectionStrings = 1,
        [Description("DefaultConnection")] DefaultDbConnection = 2,
        [Description("log4net.config")] Log4NetConfigFile = 4
    }
}