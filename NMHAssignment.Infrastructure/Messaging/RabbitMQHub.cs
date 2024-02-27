using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Infrastructure.Messaging.Configuration;
using RabbitMQ.Client;
using System.Text.Json;

namespace NMHAssignment.Infrastructure.Messaging
{
    internal class RabbitMQHub : IDisposable, IMessageHub
    {
        private IConnection? _connection;
        private IModel? _channel;

        private readonly MessageHubOptions _options;
        private readonly ILogger<RabbitMQHub> _logger;

        public RabbitMQHub(IOptions<MessageHubOptions> options, ILogger<RabbitMQHub> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public void Connect()
        {
            var factory = new ConnectionFactory
            {
                UserName = _options.UserName,
                Password = _options.Password,
                HostName = _options.Host,
                Port = _options.Port
            };

            for (int i = 0; i < _options.MaxRetryCount; i++)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    _logger.LogInformation("Successfully connected to RabbitMQ broker.");
                    return;
                }
                catch
                {
                    if (i == _options.MaxRetryCount - 1)
                    {
                        throw;
                    }
                    else
                    {
                        _logger.LogError("Failed to establish connection to RabbitMQ broker. Reconnecting...");
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                }
            }
        }

        public void Publish<T>(T data, string queueName)
        {
            var rawData = JsonSerializer.SerializeToUtf8Bytes(data);

            _channel.QueueDeclare(queueName);

            lock ( _channel )
            {
                var props = _channel.CreateBasicProperties();

                _channel.BasicPublish("", queueName, props, rawData);
            }
        }

        public void Dispose()
        {
            using (_connection)
            {
                _connection?.Close();
                _channel?.Dispose();
            }
        }
    }
}
