using System;
using Topdev.Crypto.Ethereum.Node.NodeConnectors;

namespace Topdev.Crypto.Ethereum.Node
{
    public static class NodeConnectorFactory
    {
        public static INodeConnector Create(NodeConnectionType type, string address)
        {
            INodeConnector connector;

            switch (type)
            {
                case NodeConnectionType.HTTP: 
                    connector = new HttpNodeConnector(); 
                    break;
                case NodeConnectionType.IPC: 
                    connector = new IpcNodeConnector(); 
                    break;
                default: throw new Exception("Unknown INodeConnectionType.");
            }

            connector.Address = address;

            return connector;
        }
    }
}