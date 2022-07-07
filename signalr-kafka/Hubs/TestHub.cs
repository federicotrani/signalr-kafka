using Microsoft.AspNetCore.SignalR;

namespace signalr_kafka.Hubs
{
    public class TestHub : Hub
    {
        private readonly KafkaProducer _kafkaProducer;

        public TestHub(KafkaProducer kafkaProducer, KafkaConsumer _)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task NewMessage(string username, string message)
        {
            await _kafkaProducer.Produce(username, message);
        }
    }
}
