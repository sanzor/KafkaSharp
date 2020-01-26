using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LS.Server {
    public static  class MiddlewareExtensions {

        public static IApplicationBuilder UseKafkaConsumer(this IApplicationBuilder builder) {
            builder.UseMiddleware<KafkaConsumeWare>();
            return builder;
        }
        


    }
}
