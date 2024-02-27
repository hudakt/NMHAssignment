using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NMHAssignment.Infrastructure.Persistance
{
    public static class PersistanceExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresDB"));
            });
        }

        public static void ApplyMigration(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ApplicationDbContext>();

            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                logger.LogInformation("Successfully applied migration to database.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to apply migration to database. {ex.Message}");
            }
        }
    }
}
