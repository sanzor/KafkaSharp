
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS.Models {
    public class MessageDeliveryResult {
       public bool WasSent { get; set; }
       public string Partition { get; set; }
    }
}
