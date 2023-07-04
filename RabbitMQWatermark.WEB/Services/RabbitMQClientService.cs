using RabbitMQ.Client;
using RabbitMQ.Client.Logging;

namespace RabbitMQWatermark.WEB.Services
{
    public class RabbitMQClientService
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;
        public static string ExchangeName = "ImageDirectExchange";
        public static string RoutingWatermark = "watermark-route-image";
        public static string QueueName = "queue-watermark-image";

        private readonly ILogger<RabbitMQClientService> _logger;
    }
}
