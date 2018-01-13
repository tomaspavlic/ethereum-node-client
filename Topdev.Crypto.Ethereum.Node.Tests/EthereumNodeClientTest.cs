using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Topdev.Crypto.Ethereum.Node;

namespace Topdev.Crypto.Ethereum.Node.Tests
{
    [TestClass]
    public class EthereumNodeClientTest
    {
        private readonly EthereumNodeClient _nodeClient;

        public EthereumNodeClientTest()
        {
            _nodeClient = new EthereumNodeClient(NodeConnectionType.HTTP, "http://localhost:8545");
        }

        [TestMethod]
        public async Task TestGetAccountsAsync()
        {
            var accounts = await _nodeClient.GetAccountsAsync();
            
            Assert.IsTrue(accounts.Length > 0);
        }

        [TestMethod]
        public async Task TestGetWeb3ClientVersion()
        {
            var version = await _nodeClient.GetWeb3ClientVersion();

            Assert.IsFalse(string.IsNullOrEmpty(version));
        }

        [TestMethod]
        public async Task TestGetSHA3()
        {
            var sha3 = await _nodeClient.GetSHA3("0x68656c6c6f20776f726c64");

            Assert.AreEqual("0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad", sha3);
        }
    }
}
