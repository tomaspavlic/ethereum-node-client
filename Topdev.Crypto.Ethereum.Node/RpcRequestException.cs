using System;
using Newtonsoft.Json;

namespace Topdev.Crypto.Ethereum.Node
{
    public class RpcRequestException : Exception
    {
        public RpcRequestException(string message) : base(message)
        {
        }

        public static RpcRequestException FromJson(string serializedJson)
        {
            var error = (RpcResponse<RpcError>)JsonConvert.DeserializeObject(
                serializedJson, 
                typeof(RpcResponse<RpcRequest>));

            return new RpcRequestException(error.Result.Message);
        }
    }
}