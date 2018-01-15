using Newtonsoft.Json;

namespace Topdev.Crypto.Ethereum.Node
{
    public class RpcResponse<T>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("result")]
        public T Result { get; set; }
        [JsonProperty("version")]
        public double Version { get; set; }
    }
}