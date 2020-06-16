using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Configuration;
using Pandell.Practicum.App.Domain;

namespace Pandell.Practicum.App.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
                
            optionsBuilder.UseMySql(ConfigurationFile.DefaultConnectionString,
                builder => builder.EnableRetryOnFailure(5, 
                    TimeSpan.FromSeconds(15), 
                    null!));
            
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        
        #region Class Members
        
        public DbSet<RandomSequence> RandomSequences { get; set; }
        
        #endregion    
    }
}