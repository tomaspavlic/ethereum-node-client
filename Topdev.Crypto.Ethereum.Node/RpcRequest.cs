using System.Collections.Generic;

namespace Topdev.Crypto.Ethereum.Node
{
    public class RpcRequest
    {
        public int Id { get; set; }
        public double Version { get; set; }
        public string Method { get; set; }
        public string[] Parameters { get; set; }
    }
}