
using Confluent.Kafka;
using LS.Conventions;
using LS.Interfaces;
using LS.Models;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LS.Extensions;
using System.Linq;

namespace LS.Server.Core {
    public class ProducerService : IKProducer {
        IProducer<Null,string> producer;
       
        private ILogger logger = Log.ForContext<ProducerService>();
        private readonly Kafka kafkaSettings;
        public ProducerService(IOptions<Conventions.Config>config) {
            var kconfig = new ConsumerConfig {
                BootstrapServers = config.Value.Kafka.ServerUrl,
                BrokerAddressFamily = BrokerAddressFamily.V4,
            };
            this.kafkaSettings = config.Value.Kafka;
            this.producer = new ProducerBuilder<Null, string>(kconfig).Build();

        }
        public async Task<MessageDeliveryResult> ProduceAsync(string Topic,Message msg) {
           
            if (msg == null) {
                throw new ArgumentNullException("message was null");
            }
            if (!kafkaSettings.Topics.Any(x => x == Topic)) {
                throw new ArgumentException($"Invalid Topic Name : {Topic}");
            }

            var message = new Message<Null, string> {
                Timestamp = new Timestamp(DateTime.UtcNow),
                Key = null,
                Value = msg.ToJson(),
            };
            logger.Information($"Calling Confluent Kafka API ");
            
            var delivery =await this.producer.ProduceAsync(Topic, message);
            
            return new MessageDeliveryResult { WasSent=true };
        }
    }
}
