using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Topdev.Crypto.Ethereum.Node
{
    public interface INodeConnector
    {
        string Address { set; }
        Task<string> SendRequestAsync(string request, int id);
    }
}
