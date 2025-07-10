using Confluent.Kafka;
using Google.Protobuf.WellKnownTypes;
using Lib.Setting;
using Lib.Utility;
using PROJECT.BASE.ENTITY;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using Config = Lib.Setting.Config;
using SecurityProtocol = Confluent.Kafka.SecurityProtocol;

namespace PROJECT.BASE.SERVICES
{
    public class KafkaProducer
    {
        private static IProducer<string, string> kafkaProducer;
        private static IAdminClient adminClient;
        public static IProducer<string, string> GetKafkaProducer()
        {
            //if (kafkaProducer != null)
            //    return kafkaProducer;
            string bootstrapServers = Config.CONFIGURATION_GLOBAL.Kafka.BootstrapServers.Value;
            string securityProtocol = Config.CONFIGURATION_GLOBAL.Kafka.SecurityProtocol;
            string enableSslCertificateVerification = Config.CONFIGURATION_GLOBAL.Kafka.EnableSslCertificateVerification.Value;
            string sslCaLocation = string.Empty;
            string sslCertificateLocation = string.Empty;
            string sslKeyLocation = string.Empty;
            string sslKeyPassword = Config.CONFIGURATION_GLOBAL.Kafka.SslKeyPassword.Value;
            string groupId = Config.CONFIGURATION_GLOBAL.Kafka.GroupId.Value;
            if (securityProtocol == "Ssl")
            {                
                sslCaLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCaLocation.Value}";
                sslCertificateLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCertificateLocation.Value}";
                sslKeyLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslKeyLocation.Value}";
                if (!System.IO.File.Exists(sslCaLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCaLocation}");
                if (!System.IO.File.Exists(sslCertificateLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCertificateLocation}");
                if (!System.IO.File.Exists(sslKeyLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslKeyLocation}");

            }
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                SecurityProtocol = securityProtocol == "Ssl" ? SecurityProtocol.Ssl : SecurityProtocol.Plaintext,
                EnableSslCertificateVerification = bool.Parse(enableSslCertificateVerification),
                SslCaLocation = sslCaLocation,
                SslCertificateLocation = sslCertificateLocation,
                SslKeyLocation = sslKeyLocation,
                SslKeyPassword = sslKeyPassword
            };
            kafkaProducer = new ProducerBuilder<string, string>(producerConfig).Build();
            return kafkaProducer;


        }
        public static IAdminClient GetIAdminClient()
        {
            //if (adminClient != null)
            //    return adminClient;
            string bootstrapServers = Config.CONFIGURATION_GLOBAL.Kafka.BootstrapServers.Value;
            string securityProtocol = Config.CONFIGURATION_GLOBAL.Kafka.SecurityProtocol;
            string enableSslCertificateVerification = Config.CONFIGURATION_GLOBAL.Kafka.EnableSslCertificateVerification.Value;
            string sslCaLocation = string.Empty;
            string sslCertificateLocation = string.Empty;
            string sslKeyLocation = string.Empty;
            string sslKeyPassword = Config.CONFIGURATION_GLOBAL.Kafka.SslKeyPassword.Value;
            string groupId = Config.CONFIGURATION_GLOBAL.Kafka.GroupId.Value;
            if (securityProtocol == "Ssl")
            {
                sslCaLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCaLocation.Value}";
                sslCertificateLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCertificateLocation.Value}";
                sslKeyLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslKeyLocation.Value}";
                if (!System.IO.File.Exists(sslCaLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCaLocation}");
                if (!System.IO.File.Exists(sslCertificateLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCertificateLocation}");
                if (!System.IO.File.Exists(sslKeyLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslKeyLocation}");

            }
            var adminClientConfig = new AdminClientConfig
            {
                BootstrapServers = bootstrapServers,
                SecurityProtocol = securityProtocol == "Ssl" ? SecurityProtocol.Ssl : SecurityProtocol.Plaintext,
                EnableSslCertificateVerification = bool.Parse(enableSslCertificateVerification),
                SslCaLocation = sslCaLocation,
                SslCertificateLocation = sslCertificateLocation,
                SslKeyLocation = sslKeyLocation,
                SslKeyPassword = sslKeyPassword,

            };
            adminClient = new AdminClientBuilder(adminClientConfig).Build();
            return adminClient;

        }
        public void Dispose()
        {
            kafkaProducer?.Dispose();
        }

    }
    public class KafkaConsumer
    {
        private static IConsumer<string, string> kafkaConsumer;
        public static IConsumer<string, string> GetKafkaConsumer()
        {
            //if (kafkaConsumer != null)
            //    return kafkaConsumer;
            string bootstrapServers = Config.CONFIGURATION_GLOBAL.Kafka.BootstrapServers.Value;
            string securityProtocol = Config.CONFIGURATION_GLOBAL.Kafka.SecurityProtocol;
            string enableSslCertificateVerification = Config.CONFIGURATION_GLOBAL.Kafka.EnableSslCertificateVerification.Value;
            string sslCaLocation = string.Empty;
            string sslCertificateLocation = string.Empty;
            string sslKeyLocation = string.Empty;
            string sslKeyPassword = Config.CONFIGURATION_GLOBAL.Kafka.SslKeyPassword.Value;
            string groupId = Config.CONFIGURATION_PRIVATE.Infomation.Service.Value;
            if (securityProtocol == "Ssl")
            {
                sslCaLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCaLocation.Value}";
                sslCertificateLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslCertificateLocation.Value}";
                sslKeyLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/{Config.CONFIGURATION_GLOBAL.Kafka.SslKeyLocation.Value}";
                if (!System.IO.File.Exists(sslCaLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCaLocation}");
                if (!System.IO.File.Exists(sslCertificateLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslCertificateLocation}");
                if (!System.IO.File.Exists(sslKeyLocation))
                    Console.WriteLine($"{Constant.MESSAGE_NOT_FOUND}: {sslKeyLocation}");

            }
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                SecurityProtocol = securityProtocol == "Ssl" ? SecurityProtocol.Ssl : SecurityProtocol.Plaintext,
                EnableSslCertificateVerification = bool.Parse(enableSslCertificateVerification),
                SslCaLocation = sslCaLocation,
                SslCertificateLocation = sslCertificateLocation,
                SslKeyLocation = sslKeyLocation,
                SslKeyPassword = sslKeyPassword,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            kafkaConsumer = new ConsumerBuilder<string, string>(consumerConfig)
               .SetKeyDeserializer(Deserializers.Utf8)
               .SetValueDeserializer(Deserializers.Utf8)
               .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
               .Build();
            return kafkaConsumer;

        }
        public void Dispose()
        {
            // Properly dispose of the Kafka consumer
            kafkaConsumer?.Dispose();
        }

    }
}
