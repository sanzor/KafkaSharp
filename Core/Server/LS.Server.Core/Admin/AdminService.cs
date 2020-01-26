using Confluent.Kafka;
using LS.Conventions;
using LS.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS.Server.Core {
    class AdminService : IAdminService {
        private SConfig config;
        public AdminService(IOptions<SConfig>config) {
            this.config = config.Value;
        }
        public Task AddTopicAsync(string topic) {
            throw new NotImplementedException();
        }

        public Task DeleteTopicsAsync(IEnumerable<string> topic) {
           using(IAdminClient client=new AdminClientBuilder { }
        }

        public Task<IEnumerable<string>> GetTopicsAsync() {
            throw new NotImplementedException();
        }
    }
}
