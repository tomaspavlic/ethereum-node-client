using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Topdev.Crypto.Ethereum.Node.Extensions;
using Topdev.Crypto.Ethereum.Node.Models;

namespace Topdev.Crypto.Ethereum.Node
{
    public class EthereumNodeClient
    {
        private int _id = 0;
        private readonly INodeConnector _connector;

        private int Id
        {
            get
            {
                _id++;
                return _id;
            }
        }

        public EthereumNodeClient(NodeConnectionType connectionType, string address)
        {
            _connector = NodeConnectorFactory.Create(connectionType, address);
        }

        /// <summary>
        /// Returns a list of addresses owned by client.
        /// </summary>
        /// <returns>20 Bytes - addresses owned by the client.</returns>
        public async Task<string[]> GetAccountsAsync()
        {
            var result = await InvokeAsync<string[]>("eth_accounts", null);

            return result.Result;
        }

        /// <summary>
        /// Returns the current client version.
        /// </summary>
        /// <returns>The current client version</returns>
        public async Task<string> GetWeb3ClientVersionAsync()
        {
            var response = await InvokeAsync<string>("web3_clientVersion", null);

            return response.Result;
        }

        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) of the given data.
        /// </summary>
        /// <param name="data">the data to convert into a SHA3 hash</param>
        /// <returns>The SHA3 result of the given string.</returns>
        public async Task<string> GetSHA3Async(string data)
        {
            var response = await InvokeAsync<string>("web3_sha3", data);

            return response.Result;
        }

        /// <summary>
        /// Returns the current network protocol version.
        /// </summary>
        /// <returns>The current network protocol version</returns>
        public async Task<string> GetNetVersionAsync()
        {
            var response = await InvokeAsync<string>("net_version", null);

            return response.Result;
        }

        /// <summary>
        /// Returns true if client is actively listening for network connections.
        /// </summary>
        /// <returns>Boolean - true when listening, otherwise false.</returns>
        public async Task<bool> GetNetListeningAsync()
        {
            var response = await InvokeAsync<bool>("net_listening", null);

            return response.Result;
        }

        /// <summary>
        /// Returns number of peers currenly connected to the client.
        /// </summary>
        /// <returns>QUANTITY - integer of the number of connected peers.</returns>
        public async Task<int> GetNetPeerCountAsync()
        {
            var response = await InvokeAsync<string>("net_peerCount", null);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the current ethereum protocol version.
        /// </summary>
        /// <returns>The current ethereum protocol version</returns>
        public async Task<int> GetProtocolVersionAsync()
        {
            var response = await InvokeAsync<string>("eth_protocolVersion", null);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns an object with data about the sync status or null.
        /// </summary>
        /// <returns>An object with sync status data or null, when not syncing</returns>
        public async Task<Syncing> GetSyncingAsync()
        {
            var response = await InvokeAsync<Syncing>("eth_syncing", null);

            return response.Result;
        }

        /// <summary>
        /// Returns the client coinbase address.
        /// </summary>
        /// <returns>20 bytes - the current coinbase address</returns>
        public async Task<string> GetCoinbaseAsync()
        {
            var response = await InvokeAsync<string>("eth_coinbase", null);

            return response.Result;
        }

        /// <summary>
        /// Returns true if client is actively mining new blocks.
        /// </summary>
        /// <returns>returns true of the client is mining, otherwise false</returns>
        public async Task<bool> GetMiningAsync()
        {
            var response = await InvokeAsync<bool>("eth_mining", null);

            return response.Result;
        }

        /// <summary>
        /// Returns the number of hashes per second that the node is mining with.
        /// </summary>
        /// <returns>number of hashes per second</returns>
        public async Task<int> GetHashrateAsync()
        {
            var response = await InvokeAsync<string>("eth_hashrate", null);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the current price per gas in wei.
        /// </summary>
        /// <returns>integer of the current gas price in wei</returns>
        public async Task<int> GetGasPriceAsync()
        {
            var response = await InvokeAsync<string>("eth_gasPrice", null);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of most recent block.
        /// </summary>
        /// <returns>integer of the current block number the client is on</returns>
        public async Task<int> GetBlockNumberAsync()
        {
            var response = await InvokeAsync<string>("eth_blockNumber", null);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the balance of the account of given address.
        /// </summary>
        /// <param name="address">20 Bytes - address to check for balance.</param>
        /// <param name="blockNumber">"latest", "earliest" or "pending"</param>
        /// <returns>integer of the current balance in wei</returns>
        public async Task<int> GetBalanceAsync(string address, BlockTag tag = BlockTag.Latest)
        {
            var response = await InvokeAsync<string>(
                "eth_getBalance",
                address,
                nameof(tag).ToLower());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the balance of the account of given address.
        /// </summary>
        /// <param name="address">20 Bytes - address to check for balance</param>
        /// <param name="blockNumber">integer block number</param>
        /// <returns></returns>
        public async Task<int> GetBalanceAsync(string address, int blockNumber)
        {
            var response = await InvokeAsync<string>("eth_getBalance", address, blockNumber.ToString());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the value from a storage position at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage</param>
        /// <param name="position">integer of the position in the storage</param>
        /// <param name="block">"latest", "earliest" or "pending"</param>
        /// <returns>the value at this storage position</returns>
        public async Task<string> GetStorageAtAsync(string address, int position, BlockTag block = BlockTag.Latest)
        {
            var response = await InvokeAsync<string>(
                "eth_getStorageAt", 
                address, 
                position.ToString(), 
                nameof(block).ToLower());

            return response.Result;
        }

        /// <summary>
        /// Returns the value from a storage position at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage</param>
        /// <param name="position">integer of the position in the storage</param>
        /// <param name="blockNumber">integer block number</param>
        /// <returns>the value at this storage position</returns>
        public async Task<string> GetStorageAtAsync(string address, int position, int blockNumber)
        {
            var response = await InvokeAsync<string>(
                "eth_getStorageAt", 
                address, 
                position.ToString(), 
                blockNumber.ToString());

            return response.Result;
        }

        /// <summary>
        /// Returns the number of transactions sent from an address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage</param>
        /// <param name="block">"latest", "earliest" or "pending"</param>
        /// <returns>integer of the number of transactions send from this address</returns>
        public async Task<int> GetTransactionCountAsync(string address, BlockTag block)
        {
            var response = await InvokeAsync<string>("eth_getTransactionCount", address, nameof(block).ToLower());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of transactions sent from an address.
        /// </summary>
        /// <param name="address">20 Bytes - address of the storage</param>
        /// <param name="blockNumber">integer block number</param>
        /// <returns>integer of the number of transactions send from this address</returns>
        public async Task<int> GetTransactionCountAsync(string address, int blockNumber)
        {
            var response = await InvokeAsync<string>("eth_getTransactionCount", address, blockNumber.ToString());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of transactions in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="hash">32 Bytes - hash of a block</param>
        /// <returns></returns>
        public async Task<int> GetTransactionCountByHashAsync(string hash)
        {
            var response = await InvokeAsync<string>("eth_getBlockTransactionCountByHash", hash);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of transactions in a block matching the given block number.
        /// </summary>
        /// <param name="block">"earliest", "latest" or "pending"</param>
        /// <returns>integer of the number of transactions in this block</returns>
        public async Task<int> GetTransactionCountByNumberAsync(BlockTag block)
        {
            var response = await InvokeAsync<string>("eth_getBlockTransactionCountByNumber", nameof(block).ToLower());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of transactions in a block matching the given block number.
        /// </summary>
        /// <param name="blockNumber">integer of a block number</param>
        /// <returns>integer of the number of transactions in this block</returns>
        public async Task<int> GetTransactionCountByNumberAsync(int blockNumber)
        {
            var response = await InvokeAsync<string>("eth_getBlockTransactionCountByNumber", blockNumber.ToString());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block hash.
        /// </summary>
        /// <param name="blockHash">32 Bytes - hash of a block</param>
        /// <returns>integer of the number of uncles in this block</returns>
        public async Task<int> GetUncleCountByBlockHashAsync(string blockHash)
        {
            var response = await InvokeAsync<string>("eth_getUncleCountByBlockHash", blockHash);

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block number.
        /// </summary>
        /// <param name="blockNumber">integer of a block number</param>
        /// <returns>integer of the number of uncles in this block</returns>
        public async Task<int> GetUncleCountByBlockNumberAsync(string blockNumber)
        {
            var response = await InvokeAsync<string>("eth_getUncleCountByBlockNumber", blockNumber.ToString());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns the number of uncles in a block from a block matching the given block number.
        /// </summary>
        /// <param name="block">"latest", "earliest" and "pending"</param>
        /// <returns>integer of the number of uncles in this block</returns>
        public async Task<int> GetUncleCountByBlockNumberAsync(BlockTag block)
        {
            var response = await InvokeAsync<string>("eth_getUncleCountByBlockNumber", nameof(block).ToLower());

            return response.Result.ToInt();
        }

        /// <summary>
        /// Returns code at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="block">"latest", "earliest" and "pending"</param>
        /// <returns>the code from the given address</returns>
        public async Task<string> GetCodeAsync(string address, BlockTag block)
        {
            var response = await InvokeAsync<string>("eth_getCode", address, nameof(block).ToLower());

            return response.Result;
        }

        /// <summary>
        /// Returns code at a given address.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="blockNumber">integer block number</param>
        /// <returns>the code from the given address</returns>
        public async Task<string> GetCodeAsync(string address, int blockNumber)
        {
            var response = await InvokeAsync<string>("eth_getCode", address, blockNumber.ToString());

            return response.Result;
        }

        /// <summary>
        /// The sign method calculates an Ethereum specific signature with: sign(keccak256("\x19Ethereum Signed Message:\n" + len(message) + message))).
        /// By adding a prefix to the message makes the calculated signature recognisable as an Ethereum specific signature. This prevents misuse where a malicious DApp can sign arbitrary data (e.g. transaction) and use the signature to impersonate the victim.
        /// Note the address to sign with must be unlocked.
        /// </summary>
        /// <param name="address">20 Bytes - address</param>
        /// <param name="message">N Bytes - message to sign</param>
        /// <returns>Signature</returns>
        public async Task<string> SignAsync(string address, string message)
        {
            var response = await InvokeAsync<string>("eth_sign", null);

            return response.Result;
        }

        /// <summary>
        /// Creates new message call transaction or a contract creation, if the data field contains code.
        /// </summary>
        /// <param name="from">20 Bytes - The address the transaction is send from</param>
        /// <param name="to">20 Bytes - (optional when creating new contract) The address the transaction is directed to</param>
        /// <param name="gas">(optional, default: 90000) Integer of the gas provided for the transaction execution. It will return unused gas</param>
        /// <param name="gasPrice">Integer of the gasPrice used for each paid gas</param>
        /// <param name="value">(optional) Integer of the value send with this transaction</param>
        /// <param name="data">The compiled code of a contract OR the hash of the invoked method signature and encoded parameters</param>
        /// <param name="nonce">(optional) Integer of a nonce. This allows to overwrite your own pending transactions that use the same nonce</param>
        /// <returns>32 Bytes - the transaction hash, or the zero hash if the transaction is not yet available</returns>
        public async Task<string> SendTransactionAsync(string from, string to, int gasPrice, int value, string data, int nonce, int gas = 9000)
        {
            var transaction = new Transaction()
            {
                From = from,
                To = to,
                GasPrice = gasPrice,
                Gas = gas,
                Value = value,
                Data = data,
                Nonce = nonce
            };

            var response = await InvokeAsync<string>("eth_sendTransaction", transaction);

            return response.Result;
        }

        private async Task<RpcResponse<T>> InvokeAsync<T>(string method, params object[] parameters)
        {
            if (parameters == null)
                parameters = new object[0];

            var request = GenerateRequest(method, parameters);
            string requestSerialized = JsonConvert.SerializeObject(request);
            var response = await _connector.SendRequestAsync(requestSerialized, request.Id);

            if (response.Contains("error"))
                throw RpcRequestException.FromJson(response);

            return (RpcResponse<T>)JsonConvert.DeserializeObject(response, typeof(RpcResponse<T>));
        }

        private RpcRequest GenerateRequest(string method, params object[] parameters)
        {
            return new RpcRequest()
            {
                Method = method,
                Version = 2.0,
                Id = Id,
                Parameters = parameters
            };
        }
    }
}