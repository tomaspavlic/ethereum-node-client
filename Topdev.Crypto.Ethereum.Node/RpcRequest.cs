using Newtonsoft.Json;
using System.Collections.Generic;

namespace Topdev.Crypto.Ethereum.Node
{
    public class RpcRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("version")]
        public double Version { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]
        public string[] Parameters { get; set; }
    }
}