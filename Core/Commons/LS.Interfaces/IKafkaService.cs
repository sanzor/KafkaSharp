using LS.Models;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LS.Interfaces {
    public interface IConsumer {
        public Task RunAsync(Settings filter,WebSocket socket);
    }
}
