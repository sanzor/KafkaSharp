using LS.Interfaces;
using LS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LS.Extensions;

namespace LS.Server {

    public class KafkaConsumeWare {
        private RequestDelegate next;

        private ILogger logger = Log.ForContext<KafkaConsumeWare>();

        public KafkaConsumeWare(RequestDelegate del) {
            this.next = del;
        }
        public async Task Invoke(HttpContext context, IConsumer kafkaService) {
            string corellationId = Guid.NewGuid().ToString();
            try {
                using (LogContext.PushProperty("corellationId", corellationId)) {
                    if (!context.WebSockets.IsWebSocketRequest) {
                        await this.next(context);
                        return;
                    }
                    Log.Information($"Accepting websocket");
                    var socket = await context.WebSockets.AcceptWebSocketAsync();
                    Log.Information($"Waiting for  provided filter...");
                    var settings = await socket.ReceiveAndDecodeAsync<Settings>();
                    if (settings == null) {
                        throw new ArgumentException("Invalid filter !");
                    }
                    Log.Information($"Running the consumer service...");
                    await kafkaService.RunAsync(settings, socket);
                }
            } catch (Exception ex) {
                Log.Error("", ex.Message);
            }
            
            
        }

    }
}

