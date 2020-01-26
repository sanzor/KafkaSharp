
using Confluent.Kafka;
using LS.Conventions;
using LS.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using LS.Extensions;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LS.Models;

namespace LS.Server.Core {
    public class ConsumerService : IConsumer {
        private ILogger logger = Log.ForContext<ConsumerService>();
        private Kafka KafkaConfig;

        public ConsumerService(IOptions<Conventions.SConfig> conf) {
            this.KafkaConfig = conf.Value.Kafka;
        }
        public async Task RunAsync(Settings settings, WebSocket socket) {


            var consumerGroupId = IDGenerator.Instance.GetUniqueConsumerGroupID();
            if (consumerGroupId == string.Empty) {
                throw new NotSupportedException("Could not generate new id,consumer group id already used");
            }
            var config = new ConsumerConfig {
                GroupId = consumerGroupId,
                BootstrapServers = KafkaConfig.ServerUrl,
                AutoOffsetReset = settings.OffsetReset
            };
            var iconsumer = new ConsumerBuilder<Ignore, string>(config).Build();
            logger.Information($" RunAsync: Subscribing to topics");
            iconsumer.Subscribe(KafkaConfig.Topics);
            await LoopAsync(settings, iconsumer, socket);




        }
        public async Task LoopAsync(Settings args, IConsumer<Ignore, string> consumer, WebSocket socket) {

            consumer.Subscribe(KafkaConfig.Topics);
        rego:
            try {
                while (true) {
                    try {
                        var result = consumer.Consume();
                        var message = result.Message.Value.FromJson<Message>();
                        var data = message.Encode();
                        await socket.SendAsync(data.ToArray(), WebSocketMessageType.Text, true, CancellationToken.None);

                    } catch (Exception ex) {
                        logger.Error("", ex.Message);
                        throw;
                    }
                }
            } catch (Exception ex) {
                Log.Error("", ex.Message);
                goto rego;
                throw;
            }

        }



    }
}
