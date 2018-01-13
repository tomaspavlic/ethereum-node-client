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

        public async Task<string[]> GetAccountsAsync() 
        {
            var result = await InvokeAsync<string[]>("geth_accounts", null);
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

        private async Task<RpcResponse<T>> InvokeAsync<T>(string method, params string[] parameters)
        {
            if (parameters == null)
                parameters = new string[0];

            var request = GenerateRequest(method, parameters);
            string requestSerialized = JsonConvert.SerializeObject(request);
            var response = await _connector.SendRequestAsync(requestSerialized, request.Id);

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