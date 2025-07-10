using System;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Newtonsoft.Json;
using static Confluent.Kafka.ConfigPropertyNames;
using Config = Lib.Setting.Config;

namespace PROJECT.BASE.SERVICES
{
    public class KafkaProducerServices
    {        
        
        public static int GetTotalPartitions(string topic)
        {
            var adminClient = KafkaProducer.GetIAdminClient();
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
            var topicMetadata = metadata.Topics.FirstOrDefault(t => t.Topic == topic);
            if (topicMetadata != null)
                return topicMetadata.Partitions.Count();
            return 0;
        }
        public static async Task SendMessageAsync<T>(string topic, string key, T message)
        {
            using var kafkaProducer = KafkaProducer.GetKafkaProducer();          
            var serialized_message = JsonConvert.SerializeObject(message);
            Random rnd = new Random();            
            try
            {
                var result = await kafkaProducer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = serialized_message });
                Console.WriteLine($"Delivered message to {result.Topic}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }

            //Quan trọng: chờ Kafka gửi xong các message
            kafkaProducer.Flush(TimeSpan.FromSeconds(10));
        }
        public static async Task<bool> CreateKafkaTopicAsync(string topicName)
        {            
            using var adminClient = KafkaProducer.GetIAdminClient();
            try
            {
                int numPartitions = int.Parse(Config.CONFIGURATION_GLOBAL.Kafka.CreateTopic.NumPartitions.Value);
                short replicationFactor = short.Parse(Config.CONFIGURATION_GLOBAL.Kafka.CreateTopic.ReplicationFactor.Value);
                var topicSpec = new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = numPartitions,
                    ReplicationFactor = replicationFactor
                };

                await adminClient.CreateTopicsAsync(new[] { topicSpec });
                Console.WriteLine($"Topic '{topicName}' created successfully.");
                return true;
            }
            catch (CreateTopicsException e)
            {
                if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
                {
                    Console.WriteLine($"Topic '{topicName}' already exists.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to create topic '{topicName}': {e.Results[0].Error.Reason}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }

    }
}
