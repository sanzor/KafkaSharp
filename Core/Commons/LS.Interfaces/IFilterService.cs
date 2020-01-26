using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace LS.Interfaces {
    public interface IFilterService {
         bool IsValid(string topic,string rawMessage);
    }
}
