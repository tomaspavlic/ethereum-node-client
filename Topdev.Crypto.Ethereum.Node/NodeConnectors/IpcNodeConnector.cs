using System.Threading.Tasks;

namespace Topdev.Crypto.Ethereum.Node.NodeConnectors
{
    public class IpcNodeConnector : INodeConnector
    {
        public string Address { set => throw new System.NotImplementedException(); }

        public Task<string> SendRequestAsync(string request, int id)
        {
            throw new System.NotImplementedException();
        }
    }
}