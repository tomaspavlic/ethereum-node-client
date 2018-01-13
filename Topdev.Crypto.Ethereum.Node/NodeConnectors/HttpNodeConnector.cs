using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Topdev.Crypto.Ethereum.Node.NodeConnectors
{
    public class HttpNodeConnector : INodeConnector
    {
        private string _address;
        public string Address
        {
            set
            {
                _address = value;
            }
        }

        public async Task<string> SendRequestAsync(string request, int id)
        {
            var client = new HttpClient();
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            string responseContent = "";

            try
            {
                var response = await client.PostAsync(_address, content);

                responseContent = await response.Content.ReadAsStringAsync();

                if(!response.IsSuccessStatusCode)
                    throw RpcRequestException.FromJson(responseContent);
                }
            catch (Exception exp)
            {
                
            }
            

            return responseContent;
        }
    }
}