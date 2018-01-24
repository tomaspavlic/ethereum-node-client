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
        public async Task TestGetWeb3ClientVersionAsync()
        {
            var version = await _nodeClient.GetWeb3ClientVersionAsync();

            Assert.IsFalse(string.IsNullOrEmpty(version));
        }

        [TestMethod]
        public async Task TestGetSHA3Async()
        {
            var sha3 = await _nodeClient.GetSHA3Async("0x68656c6c6f20776f726c64");

            Assert.AreEqual("0x47173285a8d7341e5e972fc677286384f802f8ef42a5ec5f03bbfa254cb01fad", sha3);
        }

        [TestMethod]
        public async Task TestGetNetVersionAsync()
        {
            var version = await _nodeClient.GetNetVersionAsync();
        }

        [TestMethod]
        public async Task TestGetNetPeerCountAsync()
        {
            int netPeerCount = await _nodeClient.GetNetPeerCountAsync();
            Assert.IsTrue(netPeerCount > 0);
        }

        [TestMethod]
        public async Task TestGetProtocolVersionAsync()
        {
            int version = await _nodeClient.GetProtocolVersionAsync();

            Assert.IsTrue(version > 0);
        }

        [TestMethod]
        public async Task TestGetSyncingAsync()
        {
            var syncing = await _nodeClient.GetSyncingAsync();

            Assert.IsNotNull(syncing);
        }

        [TestMethod]
        public async Task TestGetCoinbaseAsync()
        {
            string coinbase = await _nodeClient.GetCoinbaseAsync();

            Assert.IsFalse(string.IsNullOrEmpty(coinbase));
        }

        [TestMethod]
        public async Task TestGetMiningAsync()
        {
            bool isSyncing = await _nodeClient.GetMiningAsync();

            Assert.IsTrue(isSyncing);
        }

        [TestMethod]
        public async Task TestGetHashrateAsync()
        {
            int hashrate = await _nodeClient.GetHashrateAsync();
        }

        [TestMethod]
        public async Task TestGetGasPriceAsync()
        {
            int gasPrice = await _nodeClient.GetGasPriceAsync();

            Assert.IsTrue(gasPrice > 0);
        }
    }
}
