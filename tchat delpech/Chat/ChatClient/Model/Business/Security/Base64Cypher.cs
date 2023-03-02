using System;
using System.Text;

namespace ChatClient.Model.Business.Security
{
    public class Base64Cypher : ISecurity
    {
        private const string name = "CYPHER";
        private const string key = "azerty";

        public string Decrypt(string message)
        {
            var base64 = Convert.FromBase64String(message);
            var xor = ToXOR(base64);
            return Encoding.UTF8.GetString(xor);
        }

        public string Encrypt(string message)
        {
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            byte[] result = new byte[messageByte.Length];
            for (int i = 0; i < messageByte.Length; i++)
            {
                result[i] = ((byte)(result[i] ^ key[i % key.Length]));
            }
            return Convert.ToBase64String(result);
        }

        public string GetName()
        {
            return name;
        }

        private byte[] ToXOR(byte[] text)
        {
            byte[] result = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                result[i] = (byte)((uint)text[i] ^ (uint)key[i % key.Length]);
            }
            return result;
        }
    }
}
