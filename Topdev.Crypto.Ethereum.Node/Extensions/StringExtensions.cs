using System;

namespace Topdev.Crypto.Ethereum.Node.Extensions
{
    public static class StringExtensions
    {
        public static Int32 ToInt(this string hexString)
        {
            return int.Parse(
                hexString.Replace("0x", ""),
                System.Globalization.NumberStyles.HexNumber);
        }
    }
}