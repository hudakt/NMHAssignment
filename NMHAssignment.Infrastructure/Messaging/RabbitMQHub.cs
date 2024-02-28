using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Infrastructure.Messaging.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NMHAssignment.Infrastructure.Messaging
{
    internal class RabbitMQHub : IDisposable, IMessageHub
    {
        private IConnection? _connection;
        private IModel? _publishChannel;
        private IModel? _consumerChannel;
        private EventingBasicConsumer _consumer;

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
                    _publishChannel = _connection.CreateModel();
                    _consumerChannel = _connection.CreateModel();
                    _consumer = new EventingBasicConsumer(_consumerChannel);
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

            _publishChannel.QueueDeclare(queueName);

            lock (_publishChannel!)
            {
                var props = _publishChannel.CreateBasicProperties();

                _publishChannel.BasicPublish("", queueName, props, rawData);
            }
        }

        public string Subscribe<T>(string queueName, Action<T> handleMessage)
        {
            _consumerChannel.QueueDeclare(queueName);

            _consumer.Received += (ch, ea) =>
            {
                var rawMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(rawMessage);
                if (message != null)
                {
                    handleMessage(message);
                }
            };

            return _consumerChannel!.BasicConsume(queueName, false, _consumer);
        }

        public void Dispose()
        {
            _connection?.Close();
            _publishChannel?.Close();

            _connection?.Dispose();
            _publishChannel?.Dispose();
        }
    }
}
