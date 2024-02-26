using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NMHAssignment.Infrastructure.Persistance
{
    public static class PersistanceExtension
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            //services.AddScoped(provider => provider.GetService<ApplicationDbContext>() ?? throw new ApplicationException("Unable to create DB context"));
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }
    }
}
