using Microsoft.Extensions.DependencyInjection;
using NMHAssignment.Application.Common.Interfaces;

namespace NMHAssignment.Infrastructure.Messaging
{
    public static class MessagingExtension
    {
        public static IServiceCollection AddMessagingServices(this IServiceCollection services)
        {
            return services.AddSingleton<IMessageHub, RabbitMQHub>();
        }

        public static void ConnectToMessageHub(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var messageHub = scope.ServiceProvider.GetRequiredService<IMessageHub>() as RabbitMQHub;
            messageHub?.Connect();
        }
    }
}
