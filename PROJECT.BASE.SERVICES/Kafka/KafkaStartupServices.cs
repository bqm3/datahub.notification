using System;
using System.Threading;
using System.Threading.Tasks;
using PROJECT.BASE.ENTITY;
using PROJECT.BASE.SERVICES;

public interface IKafkaStartupService
{
    Task StartKafkaConsumerAsync(string inputValue);
}

public class KafkaStartupService : IKafkaStartupService
{
    public async Task StartKafkaConsumerAsync(string inputValue)
    {
        try
        {
            var arrTopic = inputValue.Split(":");
            string topicData = arrTopic[0];
            string sendTopic = arrTopic[1];
            bool forward = (arrTopic[2] == "1");

            if (!string.IsNullOrEmpty(sendTopic))
                await KafkaProducerServices.CreateKafkaTopicAsync(sendTopic);

            if (!string.IsNullOrEmpty(topicData))
            {
                var result = await KafkaProducerServices.CreateKafkaTopicAsync(topicData);
                if (result)
                {
                    await KafkaProducerServices.SendMessageAsync(topicData, $"KAFKA_START-{Guid.NewGuid()}", new KafkaMessage()
                    {
                        TopicName = topicData,
                        Message = "KAFKA_START"
                    });

                    Thread.Sleep(1000);
                    var cts = new CancellationTokenSource();
                    await KafkaConsumerService.StartConsuming_SyncMongoDB(topicData, sendTopic, forward, cts.Token);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"KafkaStartupService error: {ex.Message}");
        }
    }
}
