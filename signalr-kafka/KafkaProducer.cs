using Confluent.Kafka;

namespace signalr_kafka
{
    public class KafkaProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaProducer()
        {
            var pConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",                
            };

            _producer = new ProducerBuilder<string, string>(pConfig).Build();
            _topic = "test";
        }

        public async Task Produce(string username, string message)
        {
            await _producer.ProduceAsync(_topic, new Message<string, string> { Key = username, Value = message });
        }
    }
}
