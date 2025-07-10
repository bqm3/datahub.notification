using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Lib.Utility;
using Lib.Setting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PROJECT.BASE.ENTITY;
using Constant = Lib.Setting.Constant;
using PROJECT.BASE.DAO;
using Consul;
using MongoDB.Bson;
using PROJECT.BASE.CORE;
using System.Text;
using Nest;
using Aspose.Cells;
using Minio.DataModel;
using Amazon.Runtime.Internal.Transform;

namespace PROJECT.BASE.SERVICES
{
    public class KafkaConsumerService
    {
        public static void StartConsuming(string topic)
        {
            var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                // Subscribe to the Kafka topic
                kafkaConsumer.Subscribe(topic);
                // Start an infinite loop to consume messages
                while (true)
                {
                    var result = kafkaConsumer.Consume(CancellationToken.None);
                    var message = result?.Message?.Value;
                    if (message == null)
                        continue;
                    Console.WriteLine($"Received Partition: {result.Partition.Value} \n");
                    Console.WriteLine($"Received Offset: {result.Offset} \n");
                    Console.WriteLine($"Received Message.Key: {result.Message.Key} \n");
                    Console.WriteLine($"Received Topic: {topic} \n");
                    Console.WriteLine($"Received Date time: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} \n");
                    Console.WriteLine($"Received Message.Value: {result.Message.Value} \n\n\n");
                    if (result.Message.Key.IndexOf("KAFKA_START") != -1)
                        Console.WriteLine($"Received Message.Key: KAFKA_START \n");
                    else
                    {
                        var objectInfo = System.Text.Json.JsonSerializer.Deserialize<EventStreamInfo>(message.ToString());
                        //TransProcessingService.AcceptanceTrans(objectInfo.Data, topic);
                    }
                    kafkaConsumer.Commit(result);
                    //kafkaConsumer.StoreOffset(result);
                }
            }
            catch (KafkaException ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine($"Consume error: {ex.Message}");
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine($"Consume error: {ex.Message}");
            }
            finally
            {
                kafkaConsumer.Close();
            }

        }

        public static async Task StartConsuming_SyncMongoDB(string topic, string sendTopic, bool forward, CancellationToken cancellationToken)
        {
            var serviceName = Lib.Setting.Config.CONFIGURATION_PRIVATE.Infomation.Service.Value;
            using var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                kafkaConsumer.Subscribe(topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        ConsumeResult<string, string> result = kafkaConsumer.Consume(cancellationToken);
                        var message = result?.Message?.Value;
                        string code = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(message));
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            Console.WriteLine($"{topic} Received message : {Constant.MESSAGE_NOT_FOUND}");
                            continue;
                        }
                        var dynamic = new Dictionary<string, object>()
                        {
                            { "code",code },
                            { "topic",topic },
                            { "message",JsonConvert.DeserializeObject<dynamic>(message) },
                            { "timestamp",DateTime.UtcNow },
                            { "services",serviceName }
                        };
                        try
                        {
                            if (result.Message.Key != null && result.Message.Key.IndexOf("KAFKA_START") != -1)
                            {
                                Console.WriteLine($"{topic} --> KAFKA_START");
                                continue;
                            }
                            var info = await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_EXT_SOCIAL).GetInfo(topic, code);
                            if (info != null)
                            {
                                await KafkaProducerServices.SendMessageAsync($"EXT-ITG-DUPLICATE", $"{code}", dynamic);
                                //await KafkaProducerServices.SendMessageAsync($"EXT-ITG-QUALITY", $"{code}", dynamic);                                
                                continue;
                            }
                            var boolResult = await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_EXT_SOCIAL).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(dynamic)), topic);
                            if (boolResult != null && boolResult.Value)
                            {
                                //string msgi_code = Guid.NewGuid().ToString();
                                var msgi = new Dictionary<string, object>()
                                {
                                    { "code",code },
                                    { "sender_code",Constant.SENDER_CODE },
                                    { "sender_name",Constant.SENDER_NAME },
                                    { "receiver_code",Constant.RECEIVER_CODE },
                                    { "receiver_name",Constant.RECEIVER_NAME },
                                    { "message_type",topic },
                                    { "send_date",DateTime.UtcNow },
                                    { "message",Constant.MESSAGE_TRANSACTION_SUCCESS },
                                    { "status",Constant.RETURN_CODE_SUCCESS },
                                    { "timestamp",DateTime.UtcNow },
                                    { "services",serviceName }
                                };
                                var boolMSGI = await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(msgi)), $"MSGI_{DateTime.Now.ToString("yyyyMM")}");
                                if (boolMSGI != null && boolMSGI.Value)
                                {
                                    if (!string.IsNullOrWhiteSpace(sendTopic))
                                    {
                                        var arrSendTopic = sendTopic.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        if (arrSendTopic.Length > 0)
                                        {
                                            foreach (var item in arrSendTopic)
                                                await KafkaProducerServices.SendMessageAsync(item, $"{code}", dynamic);
                                            await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(new Dictionary<string, object>()
                                            {
                                                { "code",Guid.NewGuid().ToString() },
                                                { "ref_code",code },
                                                { "sender_code",Constant.SENDER_CODE },
                                                { "sender_name",Constant.SENDER_NAME },
                                                { "receiver_code",Constant.RECEIVER_CODE },
                                                { "receiver_name",Constant.RECEIVER_NAME },
                                                { "message_type",topic },
                                                { "message","send_topic" },
                                                { "status",boolMSGI.Value == true ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_FAILED },
                                                { "timestamp",DateTime.UtcNow },
                                                { "services",serviceName }
                                            })), $"MSGO_{DateTime.Now.ToString("yyyyMM")}");
                                        }
                                    }
                                    if (forward)
                                    {
                                        var resultForward = DataObjectFactory.GetInstanceS_DULIEU_FW().Add(new S_DULIEU_FW
                                        {
                                            CODE = code,
                                            DATA = message,
                                            STATUS = 1,
                                            IS_DELETE = 0,
                                            CUSER = serviceName
                                        });
                                        await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(new Dictionary<string, object>()
                                        {
                                            { "code",Guid.NewGuid().ToString() },
                                            { "ref_code",code },
                                            { "sender_code",Constant.SENDER_CODE },
                                            { "sender_name",Constant.SENDER_NAME },
                                            { "receiver_code",Constant.RECEIVER_CODE },
                                            { "receiver_name",Constant.RECEIVER_NAME },
                                            { "message_type",topic },
                                            { "message","forward" },
                                            { "status",resultForward.Value > 0 ? Constant.RETURN_CODE_SUCCESS : Constant.RETURN_CODE_FAILED },
                                            { "timestamp",DateTime.UtcNow },
                                            { "services",serviceName }
                                        })), $"MSGO_{DateTime.Now.ToString("yyyyMM")}");
                                    }

                                }
                                kafkaConsumer.Commit(result);
                            }
                        }
                        catch (KafkaException ex)
                        {
                            if (dynamic.ContainsKey("exception"))
                                dynamic["exception"] = $"{topic} Commit failed: {ex.Message}";
                            else
                                dynamic.Add("exception", $"{topic} Commit failed: {ex.Message}");
                            await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(dynamic)), "EXT_SOCIAL_LOGS");
                            Console.WriteLine($"{topic} Commit failed: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            if (dynamic.ContainsKey("exception"))
                                dynamic["exception"] = $"{topic} Error handling message: {ex.Message}";
                            else
                                dynamic.Add("exception", $"{topic} Error handling message: {ex.Message}");
                            await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(dynamic)), "EXT_SOCIAL_LOGS");
                            Console.WriteLine($"{topic} Error handling message: {ex.Message}");
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        await LogToMongo(topic, message, $"{topic} Error handling message: {ex.Message}", serviceName);
                        continue;
                    }
                    catch (OperationCanceledException)
                    {
                        string exception = $"{topic} Kafka consume Token bị hủy: thoát khỏi loop";
                        await LogToMongo(topic, message, $"{exception}", serviceName);

                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                string exception = $"{topic} Fatal error: {ex.Message}";
                await LogToMongo(topic, null, $"{exception}", serviceName);
            }
            finally
            {
                try
                {
                    kafkaConsumer.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    string exception = $"{topic} Consumer already disposed: {ex.Message}";
                    await LogToMongo(topic, message, $"{exception}", serviceName);
                }
            }
        }
        public static async Task StartConsuming_Duplicate(string topic, CancellationToken cancellationToken)
        {
            var serviceName = Lib.Setting.Config.CONFIGURATION_PRIVATE.Infomation.Service.Value;
            using var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                kafkaConsumer.Subscribe(topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        ConsumeResult<string, string> result = kafkaConsumer.Consume(cancellationToken);
                        var message = result?.Message?.Value;
                        string code = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(message));
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            Console.WriteLine($"{topic} Received message : {Constant.MESSAGE_NOT_FOUND}");
                            continue;
                        }
                        try
                        {
                            if (result.Message.Key != null && result.Message.Key.IndexOf("KAFKA_START") != -1)
                            {
                                Console.WriteLine($"{topic} --> KAFKA_START");
                                continue;
                            }
                            var dynamic = JsonConvert.DeserializeObject<dynamic>(message);
                            await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_EXT_SOCIAL).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(dynamic)), topic);
                            kafkaConsumer.Commit(result);
                        }
                        catch (KafkaException ex)
                        {
                            await LogToMongo(topic, message, $"{topic} Commit failed: {ex.Message}", serviceName);
                            Console.WriteLine($"{topic} Commit failed: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            await LogToMongo(topic, message, $"{topic} Error handling message: {ex.Message}", serviceName);
                            Console.WriteLine($"{topic} Error handling message: {ex.Message}");
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        string exception = $"{topic} Kafka consume error: {ex.Error.Reason}";
                        await LogToMongo(topic, null, $"{exception}", serviceName);
                        Console.WriteLine($"{exception}");
                        continue;
                    }
                    catch (OperationCanceledException)
                    {
                        string exception = $"{topic} Kafka consume Token bị hủy: thoát khỏi loop";
                        await LogToMongo(topic, null, $"{exception}", serviceName);
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                string exception = $"{topic} Fatal error: {ex.Message}";
                await LogToMongo(topic, null, $"{exception}", serviceName);
                Console.WriteLine($"{exception}");
            }
            finally
            {
                try
                {
                    kafkaConsumer.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    string exception = $"{topic} Consumer already disposed: {ex.Message}";
                    await LogToMongo(topic, null, $"{exception}", serviceName);
                    Console.WriteLine($"{exception}");
                }
            }
        }
        public static async Task StartConsuming_Quality(string topic, CancellationToken cancellationToken)
        {
            var serviceName = Lib.Setting.Config.CONFIGURATION_PRIVATE.Infomation.Service.Value;
            using var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                kafkaConsumer.Subscribe(topic);
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        ConsumeResult<string, string> result = kafkaConsumer.Consume(cancellationToken);
                        var message = result?.Message?.Value;
                        string code = Encrypt.ComputeMd5Hash(Encoding.UTF8.GetBytes(message));
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            Console.WriteLine($"{topic} Received message : {Constant.MESSAGE_NOT_FOUND}");
                            continue;
                        }
                        try
                        {
                            if (result.Message.Key != null && result.Message.Key.IndexOf("KAFKA_START") != -1)
                            {
                                Console.WriteLine($"{topic} --> KAFKA_START");
                                continue;
                            }
                            var dynamic = JsonConvert.DeserializeObject<dynamic>(message);
                            await DataObjectFactory.GetInstanceBaseMongo(ConstantConfig.DHUB_EXT_SOCIAL).Insert(BsonDocument.Parse(JsonConvert.SerializeObject(dynamic)), topic);
                            kafkaConsumer.Commit(result);
                        }
                        catch (KafkaException ex)
                        {
                            await LogToMongo(topic, message, $"{topic} Commit failed: {ex.Message}", serviceName);
                            Console.WriteLine($"{topic} Commit failed: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            await LogToMongo(topic, message, $"{topic} Error handling message: {ex.Message}", serviceName);
                            Console.WriteLine($"{topic} Error handling message: {ex.Message}");
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        string exception = $"{topic} Kafka consume error: {ex.Error.Reason}";
                        await LogToMongo(topic, null, $"{exception}", serviceName);
                        continue;
                    }
                    catch (OperationCanceledException)
                    {
                        string exception = $"{topic} Kafka consume Token bị hủy: thoát khỏi loop";
                        await LogToMongo(topic, null, $"{exception}", serviceName);
                        Console.WriteLine($"{exception}");
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                string exception = $"{topic} Fatal error: {ex.Message}";
                await LogToMongo(topic, null, $"{exception}", serviceName);
                Console.WriteLine($"{exception}");
            }
            finally
            {
                try
                {
                    kafkaConsumer.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    string exception = $"{topic} Consumer already disposed: {ex.Message}";
                    await LogToMongo(topic, null, $"{exception}", serviceName);
                    Console.WriteLine($"{exception}");
                }
            }
        }

        public static void StartConsuming_SyncMinIO(string topic, CancellationToken cancellationToken)
        {
            using var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                kafkaConsumer.Subscribe(topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    string syncName = "\nSyncMinIO";
                    ConsumeResult<string, string> result = null;

                    try
                    {
                        result = kafkaConsumer.Consume(cancellationToken);
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"{topic} Kafka consume error: {ex.Error.Reason}");
                        continue;
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine($"{topic} Kafka consume Token bị hủy: thoát khỏi loop");
                        break;
                    }

                    var message = result?.Message?.Value;
                    if (string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine($"{topic} Received message : {Constant.MESSAGE_NOT_FOUND}");
                        continue;
                    }
                    try
                    {
                        Console.WriteLine($"{topic} Received: {message}");
                        kafkaConsumer.Commit(result);
                    }
                    catch (KafkaException ex)
                    {
                        Console.WriteLine($"{topic} Commit failed: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{topic} Error handling message: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine($"{topic} Inner Exception: " + ex.InnerException.Message);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
            }
            finally
            {
                try
                {
                    kafkaConsumer.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    Console.WriteLine($"Consumer already disposed: {ex.Message}");
                }
            }

        }
        public static void StartConsuming_SyncWebAPI(string topic)
        {
            var kafkaConsumer = KafkaConsumer.GetKafkaConsumer();
            try
            {
                // Subscribe to the Kafka topic
                kafkaConsumer.Subscribe(topic);
                // Start an infinite loop to consume messages
                while (true)
                {
                    var result = kafkaConsumer.Consume(CancellationToken.None);
                    var message = result?.Message?.Value;
                    if (message == null)
                        continue;
                    Console.WriteLine($"Received Partition: {result.Partition.Value} \n");
                    Console.WriteLine($"Received Offset: {result.Offset} \n");
                    Console.WriteLine($"Received Message.Key: {result.Message.Key} \n");
                    Console.WriteLine($"Received Topic: {topic} \n");
                    Console.WriteLine($"Received Date time: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} \n");
                    Console.WriteLine($"Received Message.Value: {result.Message.Value} \n\n\n");
                    if (result.Message.Key.IndexOf("KAFKA_START") != -1)
                        Console.WriteLine($"Received Message.Key: KAFKA_START \n");
                    else
                    {
                        var objectInfo = System.Text.Json.JsonSerializer.Deserialize<EventStreamInfo>(message.ToString());
                        //TransProcessingService.AcceptanceTrans(objectInfo.Data, topic);
                    }
                    kafkaConsumer.Commit(result);
                    //kafkaConsumer.StoreOffset(result);
                }
            }
            catch (KafkaException ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine($"Consume error: {ex.Message}");
            }
            catch (Exception ex)
            {
                LogEventError.LogEvent(MethodBase.GetCurrentMethod(), ex);
                Console.WriteLine($"Consume error: {ex.Message}");
            }
            finally
            {
                kafkaConsumer.Close();
            }

        }

        public static async Task LogToMongo(string topic, string message, string exception, string serviceName, string collection = "EXT_SOCIAL_LOGS")
        {
            try
            {
                var log = new Dictionary<string, object>
            {
                { "topic", topic },
                { "message", message },
                { "timestamp", DateTime.UtcNow },
                { "exception", exception },
                { "services", serviceName }
            };

                var logDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(
                    Newtonsoft.Json.JsonConvert.SerializeObject(log));

                await DataObjectFactory
                    .GetInstanceBaseMongo(ConstantConfig.DHUB_DATA_LOGS)
                    .Insert(logDoc, collection);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LogToMongo ERROR] Failed to log to MongoDB: {ex.Message}");
            }
        }
    }
}
