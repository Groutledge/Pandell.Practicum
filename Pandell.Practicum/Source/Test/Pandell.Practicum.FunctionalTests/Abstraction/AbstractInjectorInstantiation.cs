using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Configuration;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.FunctionalTests.Abstraction
{
    [ExcludeFromCodeCoverage]
    public abstract class AbstractInjectorInstantiation : IDisposable
    {
        protected AbstractInjectorInstantiation()
        {
            Injector.AddApplicationDbContext(GenerateApplicationDbContext());
        }
        
        public void Dispose()
        {
            Injector.ReleaseAllRegistrations();
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