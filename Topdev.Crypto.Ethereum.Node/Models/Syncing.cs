using Newtonsoft.Json;
using Topdev.Crypto.Ethereum.Node.Converters;

namespace Topdev.Crypto.Ethereum.Node.Models
{
    public class Syncing
    {
        /// <summary>
        /// The block at which the import started (will only be reset, after the sync reached his head)
        /// </summary>
        /// <value></value>
        [JsonProperty("startingBlock"), JsonConverter(typeof(JsonHexConverter))]
        public int StartingBlock { get; set; }

        /// <summary>
        /// The current block, same as eth_blockNumber
        /// </summary>
        /// <value></value>
        [JsonProperty("currentBlock"), JsonConverter(typeof(JsonHexConverter))]
        public int CurrentBlock { get; set; }


        /// <summary>
        /// The estimated highest block
        /// </summary>
        /// <value></value>
        [JsonProperty("highestBlock"), JsonConverter(typeof(JsonHexConverter))]
        public int HighestBlock { get; set; }
    }
}