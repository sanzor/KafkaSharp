using Microsoft.Extensions.Hosting;

using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LS.Server.Core {
    class KafkaProducerService : IHostedService {
        private readonly ILogger logger;
        private readonly IHostApplicationLifetime lifetime;
        private Task produceTask;
      
        public KafkaProducerService(IHostApplicationLifetime lifetime,ILogger logger) {
            this.logger = logger;
            this.lifetime = lifetime;
        }
        public  Task StartAsync(CancellationToken cancellationToken) {
            this.lifetime.ApplicationStarted.Register(OnStarted);
            this.lifetime.ApplicationStopping.Register(OnStopping);
            this.lifetime.ApplicationStopped.Register(OnStopped);
            return Task.CompletedTask;
        }

        private void OnStopping() {
            this.logger.Information($"[] Stopping producer");
        }

        private void OnStopped() {
            this.logger.Information($"[] Producer is stopped");
        }
        private void OnStarted() {
            var guid = Guid.NewGuid().ToString();
            TaskScheduler.UnobservedTaskException +=(s,e)=> TaskScheduler_UnobservedTaskException(s,e,guid);
            try {
                this.produceTask = Task.Run(LoopAsync);
            } catch (Exception ex) {
               
                throw;
            }
            
            this.logger.Information($"[] Producer started");

        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e,string corellationId) {
            Log.Error($"[{corellationId}] - Producer threw",e.Exception.Message);
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            
            return Task.CompletedTask;
        }

        private async Task LoopAsync() {

            logger.Information("");
        }
    }
}
