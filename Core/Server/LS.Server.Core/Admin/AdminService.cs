using LS.Conventions;
using LS.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS.Server.Core {
    class AdminService : IAdminService {
       
        public AdminService(IOptions<Config>config) {

        }
        public Task AddTopicAsync(string topic) {
            throw new NotImplementedException();
        }

        public Task DeleteTopicsAsync(IEnumerable<string> topic) {
           using()
        }

        public Task<IEnumerable<string>> GetTopicsAsync() {
            throw new NotImplementedException();
        }
    }
}
