using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LS.Models {
    public class Settings {
        [JsonPropertyName("offset")]
        public AutoOffsetReset OffsetReset { get; set; } = AutoOffsetReset.Earliest;
       
        [JsonPropertyName("topics")]
        public IEnumerable<string> Topics { get; set; }
    }
    
}
