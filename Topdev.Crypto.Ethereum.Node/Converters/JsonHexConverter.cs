using System;
using Newtonsoft.Json;
using Topdev.Crypto.Ethereum.Node.Extensions;

namespace Topdev.Crypto.Ethereum.Node.Converters
{
    public class JsonHexConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(string))
                return true;

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ((string)reader.Value).ToInt();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            int number = (int)value;

            writer.WriteValue(string.Format("0x{0:X}", number));
        }
    }
}