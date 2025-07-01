using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace microservice.mess.Kafka
{
    public class KafkaProducerService
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducerService()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "host.docker.internal:9092",
                Acks = Acks.All
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendMessageAsync(string topic, string key, string message)
        {
            var result = await _producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = key,
                Value = message
            });

            Console.WriteLine($"Sent to Kafka: {result.TopicPartitionOffset}");
        }
    }
}