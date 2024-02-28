
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Application.DTOs;

namespace NMHAssignment.WebAPI.Services
{
    public class MessageHandleService : BackgroundService
    {
        private readonly IMessageHub _messageHub;
        private readonly ILogger<MessageHandleService> _logger;

        public MessageHandleService(IMessageHub messageHub, ILogger<MessageHandleService> logger)
        {
            _messageHub = messageHub;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageHub.Subscribe<CalculationDTO>("calculation_queue", (message) =>
            {
                _logger.LogInformation(message.ToString());
            });

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
