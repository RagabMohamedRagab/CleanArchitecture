using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CleanArchitecture.Service.Dtos;
using CleanArchitecture.Service.IMangers.IRabbitMQManger;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CleanArchitecture.Service.Managers.RabbitMQManger
{
    public class RabbitMQ(
        IConnection _connection,
        IChannel _channel,
        ILogger<RabbitMQ> _logger,
        RabbitMQConfig _rabbitMQConfig) : IRabbitMQ, IDisposable
    {
        public async Task Producer(string Message, string QueueName)
        {
            var factoryConnection = new ConnectionFactory() {
                HostName = _rabbitMQConfig.Host,      // عنوان السيرفر
                Port = int.Parse(_rabbitMQConfig.Port),
                UserName = _rabbitMQConfig.Username,   // اسم المستخدم
                Password = _rabbitMQConfig.Password,   // كلمة السر
                VirtualHost = _rabbitMQConfig.VirtualHost ?? "/"
            };
            try {
                _connection = await factoryConnection.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
                // إنشاء الـ Queue لو مش موجودة
                await _channel.QueueDeclareAsync(
                       queue: QueueName,        // اسم الـ Queue
                       durable: true,           // تفضل موجودة حتى لو السيرفر أُعيد تشغيله
                       exclusive: false,        // مش حصرية لاتصال واحد
                       autoDelete: false,       // ما تتمسحش تلقائياً
                       arguments: null
                   );


                var jsonMessage = JsonSerializer.Serialize(Message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                var properties = new BasicProperties();
                properties.Persistent = true;

                await _channel.BasicPublishAsync(
                   exchange: "",
                   routingKey: QueueName,
                   mandatory: false,
                   basicProperties: properties,
                   body: body
               );

            }
            catch (Exception ex) {

            }
        }

        public async Task SubscribeAsync(string queueName)
        {
            try {
                await _channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.ReceivedAsync += async (model, ea) => {
                    try {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var deserializedMessage = JsonSerializer.Deserialize<string>(message);

                     
                    }
                    catch (Exception ex) {
                        _logger.LogError(ex, "Error processing message");
                        // Reject and requeue the message
                        await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                    }
                };

                await _channel.BasicConsumeAsync(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer
                );

                _logger.LogInformation($"Subscribed to queue '{queueName}'");
            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Failed to subscribe to queue '{queueName}'");
                throw;
            }
        }
        public void Dispose()
        {
            _channel?.CloseAsync();
            _connection?.CloseAsync();
        }
    }
}
