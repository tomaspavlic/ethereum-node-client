using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public async Task<string> GetWeb3ClientVersion()
        {
            var response = await InvokeAsync<string>("web3_clientVersion", null);
            return response.Result;
        }

        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) of the given data.
        /// </summary>
        /// <param name="data">the data to convert into a SHA3 hash</param>
        /// <returns>The SHA3 result of the given string.</returns>
        public async Task<string> GetSHA3(string data)
        {
            var response = await InvokeAsync<string>("web3_sha3", data);
            return response.Result;
        }

        /// <summary>
        /// Returns the current network protocol version.
        /// </summary>
        /// <returns>The current network protocol version</returns>
        public async Task<string> GetNetVersion()
        {
            var response = await InvokeAsync<string>("net_version", null);
            return response.Result;
        }

        /// <summary>
        /// Returns true if client is actively listening for network connections.
        /// </summary>
        /// <returns>Boolean - true when listening, otherwise false.</returns>
        public async Task<bool> GetNetListening()
        {
            var response = await InvokeAsync<bool>("net_listening", null);
            return response.Result;
        }

        /// <summary>
        /// Returns number of peers currenly connected to the client.
        /// </summary>
        /// <returns>QUANTITY - integer of the number of connected peers.</returns>
        public async Task<int> GetNetPeerCount()
        {
            var response = await InvokeAsync<string>("net_peerCount", null);

            int netPeerCount = int.Parse(
                response.Result.Replace("0x", ""), 
                System.Globalization.NumberStyles.HexNumber);

            return netPeerCount;
        }


        private async Task<RpcResponse<T>> InvokeAsync<T>(string method, params string[] parameters)
        {
            if (parameters == null)
                parameters = new string[0];

            var request = GenerateRequest(method, parameters);
            string requestSerialized = JsonConvert.SerializeObject(request);
            var response = await _connector.SendRequestAsync(requestSerialized, request.Id);

            if(response.Contains("error"))
                throw RpcRequestException.FromJson(response);

            return (RpcResponse<T>)JsonConvert.DeserializeObject(response, typeof(RpcResponse<T>));
        }

        private RpcRequest GenerateRequest(string method, params string[] parameters)
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