using System;
using System.Text;

namespace ChatClient.Model.Business.Security
{
    public class Base64Cypher : ISecurity
    {
        private const string name = "CYPHER";

        public string Decrypt(string message)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(message));
        }

        public string Encrypt(string message)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        }

        public string GetName()
        {
            return name;
        }
    }
}
