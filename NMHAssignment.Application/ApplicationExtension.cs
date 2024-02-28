using Microsoft.Extensions.DependencyInjection;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Application.Services;

namespace NMHAssignment.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
            services.AddSingleton<ICalculationService, CalculationService>()
                    .AddMemoryCache();
    }
}
