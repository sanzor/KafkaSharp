using LS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LS.Interfaces {
    public interface IKProducer {
        Task<MessageDeliveryResult> ProduceAsync(string Topic,Message msg);
    }
}
