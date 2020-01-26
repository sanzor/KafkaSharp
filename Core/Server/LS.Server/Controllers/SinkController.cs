using LS.Conventions;
using LS.Interfaces;
using LS.Models;
using LS.Routes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Context;
using System;
using System.Text.Json;
using System.Threading.Tasks;


namespace LS.Server {
    [ApiController]
    [Route("[controller]")]
    public class SinkController : Controller {
        private ILogger logger = Log.ForContext<SinkController>();
        private IKProducer producerService = null;

        public SinkController(IKProducer prod) {
            this.producerService = prod;
        }
        [HttpPost]
        [Route(Routes.Routes.EventTopic)]
        public async Task<ActionResult<bool>> ProduceEventAsync(Message msg) {

            string corellationId = Guid.NewGuid().ToString();
            using (LogContext.PushProperty(Constants.CORELLATION_ID, corellationId)) {
                logger.Information($"Request starting...");
                try 
                    {
                    var rez = await this.producerService.ProduceAsync("event", msg);
                    return StatusCode(200, rez);
                } catch (Exception ex) {
                    logger.Error($"[{corellationId}] ProduceMessageAsync:  Threw..", ex.Message);
                    return StatusCode(500, ex.Message);
                }
            }


        }

        [HttpPost]
        [Route(Routes.Routes.ErrorTopic)]
        public async Task<ActionResult<bool>> ProduceErrorAsync(Message msg) {
            string corellationId = Guid.NewGuid().ToString();
            using (LogContext.PushProperty(Constants.CORELLATION_ID, corellationId)) {
                try {
                    logger.Information($"Request starting...");
                    var rez = await this.producerService.ProduceAsync("error", msg);
                    return StatusCode(200, rez);
                } catch (Exception ex) {
                    logger.Error($"[{corellationId}] ProduceMessageAsync:  Threw..", ex.Message);
                    return StatusCode(500, ex.Message);
                }
            }
        }

    }
}
