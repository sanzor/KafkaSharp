using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LS.Conventions {
    public class Kafka {

        [JsonPropertyName("serverUrl")]
        public string ServerUrl { get; set; }

        [JsonPropertyName("topics")]
        public string[] Topics { get; set; }
        
    }
}
