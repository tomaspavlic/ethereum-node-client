using Newtonsoft.Json;
using Topdev.Crypto.Ethereum.Node.Converters;

namespace Topdev.Crypto.Ethereum.Node.Models
{
    internal class Transaction
    {
        /// <summary>
        /// 20 Bytes - The address the transaction is send from
        /// </summary>
        /// <value></value>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// 20 Bytes - (optional when creating new contract) The address the transaction is directed to
        /// </summary>
        /// <value></value>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// (optional, default: 90000) Integer of the gas provided for the transaction execution. It will return unused gas
        /// </summary>
        /// <value></value>
        [JsonProperty("gas"), JsonConverter(typeof(JsonHexConverter))]
        public int Gas { get; set; }

        /// <summary>
        /// (optional, default: To-Be-Determined) Integer of the gasPrice used for each paid gas
        /// </summary>
        /// <value></value>
        [JsonProperty("gasPrice"), JsonConverter(typeof(JsonHexConverter))]
        public int GasPrice { get; set; }

        /// <summary>
        /// (optional) Integer of the value send with this transaction
        /// </summary>
        /// <value></value>
        [JsonProperty("value"), JsonConverter(typeof(JsonHexConverter))]
        public int Value { get; set; }

        /// <summary>
        /// The compiled code of a contract OR the hash of the invoked method signature and encoded parameters
        /// </summary>
        /// <value></value>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// (optional) Integer of a nonce. This allows to overwrite your own pending transactions that use the same nonce
        /// </summary>
        /// <value></value>
        [JsonProperty("nonce")]
        public int Nonce { get; set; }
    }
}