using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LS.Conventions {
   
    [Serializable]
    public partial class LSConfig {
        [JsonPropertyName("serverUrl")]
        public string ServerUrl { get; set; }
        [JsonPropertyName("swagger")]
        public Swagger Swagger { get; set; }
        [JsonPropertyName("kafka")]
        public Kafka Kafka { get; set; }
    }
}


