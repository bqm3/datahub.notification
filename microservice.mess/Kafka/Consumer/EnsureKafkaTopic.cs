using Confluent.Kafka.Admin;
using Confluent.Kafka;

namespace microservice.mess.Kafka.Consumer
{
    public class EnsureKafkaTopic
    {
        public static async Task EnsureKafkaTopicsAsync(string bootstrapServers, string[] requiredTopics)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();

            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
            var existingTopics = metadata.Topics.Select(t => t.Topic).ToHashSet();

            var topicsToCreate = requiredTopics
                .Where(t => !existingTopics.Contains(t))
                .Select(t => new TopicSpecification { Name = t, NumPartitions = 1, ReplicationFactor = 1 })
                .ToList();

            if (topicsToCreate.Any())
            {
                await adminClient.CreateTopicsAsync(topicsToCreate);
                Console.WriteLine($" Created topics: {string.Join(", ", topicsToCreate.Select(t => t.Name))}");
            }
        }
    }

}