using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Configuration;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.FunctionalTests.Abstraction
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractInjectorInstantiation
    {
        protected AbstractInjectorInstantiation()
        {
            Injector.AddApplicationDbContext(GenerateApplicationDbContext());
        }
        
        private ApplicationDbContext GenerateApplicationDbContext()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConfigurationFile.DefaultConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            
            return new ApplicationDbContext(dbOptions);
        }
    }
}