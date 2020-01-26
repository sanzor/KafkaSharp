
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LS.Models {
    [Serializable]
    public  class Message {
        [JsonPropertyName("payload")]
         public object Payload { get; set; }


       
    }
    
    
    
    
}
