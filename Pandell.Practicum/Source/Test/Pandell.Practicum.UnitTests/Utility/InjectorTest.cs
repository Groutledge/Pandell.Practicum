using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Configuration;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Repository;
using Pandell.Practicum.App.Services;
using Pandell.Practicum.App.Utility;
using Xunit;

namespace Pandell.Practicum.UnitTests.Utility
{
    [ExcludeFromCodeCoverage]
    public class InjectorTest
    {
        [Fact]
        public void AddApplicationDbContext_ReturnsTheWindsorCollection_WithTheApplicationDbContext()
        {
            Injector.IsAlreadyRegistered().Should().BeFalse();
            
            AssertContainerOnApplyingDatabaseContext();
            AssertCertainInstancesInsideContainer();

            Injector.ReleaseAllRegistrations();
        }

        private void AssertContainerOnApplyingDatabaseContext()
        {
            var applicationDbContext = GenerateApplicationDbContext();
            Injector.AddApplicationDbContext(applicationDbContext);
            Injector.IsAlreadyRegistered().Should().BeTrue();
        }

        private static void AssertCertainInstancesInsideContainer()
        {
            var repository = Injector.Resolve<IRepository<RandomSequence, Guid>>();
            var service = Injector.Resolve<IRandomSequenceService>();

            (repository.GetType() == typeof(RandomSequenceRepository)).Should().BeTrue();
            (service.GetType() == typeof(RandomSequenceService)).Should().BeTrue();
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