﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQWatermark.WEB.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitmqClientService = rabbitMQClientService;
        }
        public void Publisher(productImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange:RabbitMQClientService.ExchangeName,routingKey:RabbitMQClientService.RoutingWatermark,basicProperties:properties,body:bodyByte);
        }
    }
}
