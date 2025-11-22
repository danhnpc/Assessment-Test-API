using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Assessment_Test_DAL.Utils;

public static class DataDependencyHelper
{
    public static void AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your data layer specific dependencies here
        // For example, registering repositories, DbContext, etc.

        services.AddFluentMigratorCore().ConfigureRunner(rb => rb.AddPostgres()
            .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
            .ScanIn(typeof(ApplicationDbContext).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));




    }

    public static void UseDataAccessLayer(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            db.MigrateUp();
        }

    }
}
