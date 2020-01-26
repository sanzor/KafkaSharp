using Confluent.Kafka;
using LS.Conventions;
using LS.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Server.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AdminController:Controller {
        private IAdminService adminService;
        private readonly ILogger logger = Log.ForContext<AdminController>();
        public AdminController(IAdminService adminService) {
            this.adminService = adminService;
        }
        [HttpPost]
        public async Task ClearTopicsAsync([FromBody]IEnumerable<string> topics) {
            try {
                string corellationId = Guid.NewGuid().ToString();
                using (LogContext.PushProperty(Constants.CORELLATION_ID, corellationId)) {
                    logger.Information($"Request starting...");
                    try {
                         await this.adminService.DeleteTopicsAsync(topics);
                    } catch (Exception ex) {
                        logger.Error($"[{corellationId}] ProduceMessageAsync:  Threw..", ex.Message);
                    }
                }
                await this.adminService.DeleteTopicsAsync(topics);
            } catch (Exception) {

                throw;
            }
            
        }
    }
}
