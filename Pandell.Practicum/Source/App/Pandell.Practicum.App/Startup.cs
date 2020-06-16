using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pandell.Practicum.App.Configuration;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDbContexts(services);
            services.AddControllersWithViews();
        }
        
        private void ConfigureDbContexts(IServiceCollection services)
        {
            Injector.AddApplicationDbContext(new ApplicationDbContext(GenerateDbContextOptions()));
            services.AddDbContext<ApplicationDbContext>(options => GenerateDbContextOptions());
        }
        
        private DbContextOptions<ApplicationDbContext> GenerateDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseMySql(ConfigurationFile.DefaultConnectionString)
                .Options;
        }
        
        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseDatabaseErrorPage();
            }
            else
            {
                applicationBuilder.UseExceptionHandler("/Home/Error");
                applicationBuilder.UseHsts();
            }

            SetupMvcServices(applicationBuilder);
        }

        private void SetupMvcServices(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #region Class Members
        
        public IConfiguration Configuration { get; }
        
        #endregion
    }
}