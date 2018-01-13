namespace Topdev.Crypto.Ethereum.Node
{
    public class RpcResponse<T>
    {
        public int Id { get; set; }
        public T Result { get; set; }
        public double Version { get; set; }
    }
}