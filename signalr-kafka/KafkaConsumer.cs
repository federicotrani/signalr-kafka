using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using signalr_kafka.Hubs;

namespace signalr_kafka
{
    public class KafkaConsumer
    {
        public KafkaConsumer(IHubContext<TestHub> hub)
        {
            Task messageReceived(string username, string message) => hub.Clients.All.SendAsync("messageReceived", username, message);

            Task.Run(async () =>
            {
                var cConfig = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",                    
                    GroupId = Guid.NewGuid().ToString(),
                    AutoOffsetReset = AutoOffsetReset.Latest
                };

                using (var consumer = new ConsumerBuilder<string, string>(cConfig).Build())
                {
                    consumer.Subscribe("test");

                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume();
                            await messageReceived(consumeResult.Message.Key, consumeResult.Message.Value);
                            Console.WriteLine($"consumed: {consumeResult.Message.Value}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"consume error: {e.Message}");
                        }
                    }
                }
            });
        }
    }
}
